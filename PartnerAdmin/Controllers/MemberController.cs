using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using biz.CommonBiz;
using biz.ExceptionBiz;
using biz.MainBiz.Member;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PartnerAdmin.Models.Json;
using PartnerAdmin.Models.Member;

namespace PartnerAdmin.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class MemberController : Controller
    {
        private static string _emailCheckString = CommonConfigurationBuilderBiz.Configuration.GetSection("valid:email").Value;
        private static string _emailCheckErrorMessage = CommonConfigurationBuilderBiz.Configuration.GetSection("valid:emailMessage").Value;

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                RedirectToAction("Main", "Main");
            }

            ViewBag.EmailCheckString = _emailCheckString;
            ViewBag.EmailCheckErrorMessage = _emailCheckErrorMessage;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Member member)
        {
            MemberBiz memberBiz = new MemberBiz();
            CommonValidationCheckBiz validationBiz = new CommonValidationCheckBiz();

            if (!validationBiz.IsEmailCheck(member.Email))
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, "올바른 형식이 아닌 이메일입니다."));
            }

            if (!validationBiz.IsContectEmailCheck(member.Email))
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, _emailCheckErrorMessage));
            }

            member.EncPassword = CommonSHA256GenerateBiz.EncryptionSHA256String(member.Password);
            List<Member> memberList = memberBiz.GetMemberInfo<Member>(member.Email, member.EncPassword);

            if (memberList.Count != 1)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, "아이디 또는 패스워드를 확인 해 주세요."));
            }
            else
            {
                var claims = new Claim[]
                {
                    new Claim("Email", memberList[0].Email),
                    new Claim("Name", memberList[0].Name)
                };

                var ci = new ClaimsIdentity(claims, member.EncPassword);

                await HttpContext.Authentication.SignInAsync("LoginCookie", new ClaimsPrincipal(ci));

                return Json(JsonResultString.GetJsonResultStringConvert(true, "로그인성공"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmail([FromBody] Member member)
        {
            try
            {
                dynamic errorList = null;
                MemberBiz memberBiz = new MemberBiz();
                CommonSendEmailBiz sendMailBiz = new CommonSendEmailBiz();
                CommonValidationCheckBiz validationBiz = new CommonValidationCheckBiz();

                if (!ModelState.IsValid)
                {
                    errorList = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    ).ToList();

                    return Json(JsonResultString.GetJsonResultStringConvert(false, errorList));
                }

                if (!validationBiz.IsContectEmailCheck(member.Email))
                {
                    Dictionary<string, string> errDictionary = new Dictionary<string, string>();
                    string message = CommonConfigurationBuilderBiz.Configuration.GetSection("valid:emailMessage").Value;
                    errDictionary.Add("email", message);
                    errorList = errDictionary.ToList();

                    return Json(JsonResultString.GetJsonResultStringConvert(false, errorList));
                }

                List<Member> memberList = memberBiz.GetMemberInfo<Member>(member.Email);
                if (memberList.Count > 0)
                {
                    throw new UserException("이미 가입한 회원입니다.");
                }

                string authCode = CommonSHA256GenerateBiz.EncryptionSHA256String(member.Email + DateTime.Now.ToString("yyyyMMddHHmmss"));
                string querystringEncode = string.Format("Email={0}&AuthCode={1}", WebUtility.UrlEncode(member.Email), WebUtility.UrlEncode(authCode));
                HttpContext.Session.SetString("authCode", authCode);

                memberBiz.RegisterAuthMember(member.Email, authCode);

                StringBuilder mailMessage = new StringBuilder();
                mailMessage.AppendFormat("<meta http-equiv='Content-Type' content='text/html; charset = utf-8'>");
                mailMessage.AppendFormat("안녕하세요. {0}님 <br />", member.Name);
                mailMessage.AppendFormat("아래의 링크를 클릭하여 이메일을 인증 해 주세요.");
                mailMessage.AppendFormat("<div style='padding-top:20px; padding-bottom: 30px;'>");
                mailMessage.AppendFormat("<a href='http://www.partneradmin.com/Member/AuthMember?{0}' " +
                    "style ='display:inline-block;color:#fff;background-color:#d40100;padding:14px;font-weight:bold;text-decoration:none;font-family: Arial;'>", querystringEncode);
                mailMessage.AppendFormat("PartnerAdmin 인증하기");
                mailMessage.AppendFormat("</a></div>");
                mailMessage.AppendFormat("감사합니다.");

                await sendMailBiz.SendEmailAsync(member.Email, "", "[PartnerAdmin]회원가입 승인 메일", mailMessage.ToString());

                
                return Json(JsonResultString.GetJsonResultStringConvert(true, "이메일 승인 해 주세요"));
            }
            catch (UserException u_ex)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, u_ex.Message));
            }
            catch (Exception ex)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, "회원가입실패"));
            }
        }

        [HttpPost]
        public IActionResult MailAuthentication([FromBody] Member member)
        {
            try
            {
                MemberBiz memberBiz = new MemberBiz();
                DateTime currentTime = DateTime.Now;
                var authCode = HttpContext.Session.GetString("authCode");
                List<AuthMember> memberAcList = memberBiz.GetAuthMemberList<AuthMember>(member.Email, authCode);

                if (memberAcList.Count == 1)
                {
                    if (memberAcList.Where(w => w.IsAuth == true).Count() > 0)
                    {
                        member.EncPassword = CommonSHA256GenerateBiz.EncryptionSHA256String(member.Password);
                        memberBiz.RegisterMember(member.Email, member.EncPassword, member.Name); //회원가입
                        HttpContext.Session.Remove("authCode");
                        return Json(JsonResultString.GetJsonResultStringConvert(true, "회원가입 완료"));
                    }
                    else
                    {
                        return Json(JsonResultString.GetJsonResultStringConvert(true, "인증대기"));
                    }
                }
                else
                {
                    throw new UserException("인증 대상에 없습니다");
                }
            }
            catch (UserException u_ex)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, u_ex.Message));
            }
            catch (Exception ex)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, "회원가입 실패"));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("LoginCookie");

            return RedirectToAction("Login", "Member");
        }

        [HttpGet]
        public IActionResult AuthMember(AuthMember memberAc)
        {
            try
            {
                MemberBiz memberBiz = new MemberBiz();
                List<AuthMember> memberAcList = new List<AuthMember>();

                memberAcList = memberBiz.GetAuthMemberList<AuthMember>(memberAc.Email);

                string compareActivationCode = memberAcList.Where(w => w.IsAuth == false).OrderByDescending(o => o.InsDate).FirstOrDefault().AuthCode;
                if (compareActivationCode == memberAc.AuthCode)
                {
                    int intMemberActivation = memberBiz.ChangeAuthMember(memberAc.Email, memberAc.AuthCode);
                    ViewBag.Result = "인증되었습니다.";
                }
                else
                {
                    throw new UserException("인증 만료입니다.");
                }
            }
            catch (UserException u_ex)
            {
                ViewBag.Result = u_ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Result = "인증 에러입니다.";
            }

            return View();
        }

        public async Task<IActionResult> ForgotPassword([FromBody] Member member)
        {
            try
            {
                MemberBiz memberBiz = new MemberBiz();
                CommonSendEmailBiz sendMailBiz = new CommonSendEmailBiz();
                CommonValidationCheckBiz validationBiz = new CommonValidationCheckBiz();

                List<Member> memberList = memberBiz.GetMemberInfo<Member>(member.Email);
                if (memberList.Count < 1)
                {
                    throw new UserException("존재하지 않는 회원입니다.");
                }

                string randomPassword = CommonGeneratePasswordBiz.CreateRandomPassword(10);
                member.NewEncPassword = CommonSHA256GenerateBiz.EncryptionSHA256String(randomPassword);
                memberBiz.UpdateMemberTempPassword(member.Email, member.NewEncPassword); //패스워드 변경

                StringBuilder mailMessage = new StringBuilder();
                mailMessage.AppendFormat("<meta http-equiv='Content-Type' content='text/html; charset = utf-8'>");
                mailMessage.AppendFormat("안녕하세요. {0}님 <br />", memberList[0].Name);
                mailMessage.AppendFormat("아래에 임시비밀번호를 발급해드렸습니다.<br />");
                mailMessage.AppendFormat("{0}<br />", randomPassword);
                mailMessage.AppendFormat("감사합니다.");

                await sendMailBiz.SendEmailAsync(member.Email, "", "[PartnerAdmin]임시 비밀번호 발급", mailMessage.ToString());

                return Json(JsonResultString.GetJsonResultStringConvert(true, "입력하신 아이디 이메일로 임시비밀번호가 발급되었습니다."));
            }
            catch (UserException u_ex)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, u_ex.Message));
            }
            catch (Exception ex)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, "비밀번호 발급 실패"));
            }
        }
        public IActionResult MemberEdit()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                Response.Redirect("/Member/Login");
            }

            return View();
        }

        public IActionResult EditPassword([FromBody] Member member)
        {
            try
            {
                MemberBiz memberBiz = new MemberBiz();
                CommonSendEmailBiz sendMailBiz = new CommonSendEmailBiz();

                member.Email = User.Claims.Where(s => s.Type == "Email").Select(s => s.Value).FirstOrDefault();
                member.EncPassword = CommonSHA256GenerateBiz.EncryptionSHA256String(member.Password);
                member.NewEncPassword = CommonSHA256GenerateBiz.EncryptionSHA256String(member.NewPassword);

                List<Member> memberList = memberBiz.GetMemberInfo<Member>(member.Email, member.EncPassword);
                if (memberList.Count < 1)
                {
                    throw new UserException("비밀번호를 확인 해 주세요.");
                }

                int result = memberBiz.UpdateMemberPassword(member.Email, member.EncPassword, member.NewEncPassword); //패스워드 변경

                if(result != 1)
                {
                    throw new UserException("비밀번호 변경 중 실패.");
                }

                return Json(JsonResultString.GetJsonResultStringConvert(true, "비밀번호가 변경 되었습니다."));
            }
            catch (UserException u_ex)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, u_ex.Message));
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return Json(JsonResultString.GetJsonResultStringConvert(false, "비밀번호 변경 실패"));
            }
        }

        public IActionResult MemberInfo()
        {
            return View();
        }
    }
}