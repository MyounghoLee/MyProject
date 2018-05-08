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
                return Json(JsonResultString.GetJsonResultStringConvert(false, "�ùٸ� ������ �ƴ� �̸����Դϴ�."));
            }

            if (!validationBiz.IsContectEmailCheck(member.Email))
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, _emailCheckErrorMessage));
            }

            member.EncPassword = CommonSHA256GenerateBiz.EncryptionSHA256String(member.Password);
            List<Member> memberList = memberBiz.GetMemberInfo<Member>(member.Email, member.EncPassword);

            if (memberList.Count != 1)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, "���̵� �Ǵ� �н����带 Ȯ�� �� �ּ���."));
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

                return Json(JsonResultString.GetJsonResultStringConvert(true, "�α��μ���"));
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
                    throw new UserException("�̹� ������ ȸ���Դϴ�.");
                }

                string authCode = CommonSHA256GenerateBiz.EncryptionSHA256String(member.Email + DateTime.Now.ToString("yyyyMMddHHmmss"));
                string querystringEncode = string.Format("Email={0}&AuthCode={1}", WebUtility.UrlEncode(member.Email), WebUtility.UrlEncode(authCode));
                HttpContext.Session.SetString("authCode", authCode);

                memberBiz.RegisterAuthMember(member.Email, authCode);

                StringBuilder mailMessage = new StringBuilder();
                mailMessage.AppendFormat("<meta http-equiv='Content-Type' content='text/html; charset = utf-8'>");
                mailMessage.AppendFormat("�ȳ��ϼ���. {0}�� <br />", member.Name);
                mailMessage.AppendFormat("�Ʒ��� ��ũ�� Ŭ���Ͽ� �̸����� ���� �� �ּ���.");
                mailMessage.AppendFormat("<div style='padding-top:20px; padding-bottom: 30px;'>");
                mailMessage.AppendFormat("<a href='http://www.partneradmin.com/Member/AuthMember?{0}' " +
                    "style ='display:inline-block;color:#fff;background-color:#d40100;padding:14px;font-weight:bold;text-decoration:none;font-family: Arial;'>", querystringEncode);
                mailMessage.AppendFormat("PartnerAdmin �����ϱ�");
                mailMessage.AppendFormat("</a></div>");
                mailMessage.AppendFormat("�����մϴ�.");

                await sendMailBiz.SendEmailAsync(member.Email, "", "[PartnerAdmin]ȸ������ ���� ����", mailMessage.ToString());

                
                return Json(JsonResultString.GetJsonResultStringConvert(true, "�̸��� ���� �� �ּ���"));
            }
            catch (UserException u_ex)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, u_ex.Message));
            }
            catch (Exception ex)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, "ȸ�����Խ���"));
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
                        memberBiz.RegisterMember(member.Email, member.EncPassword, member.Name); //ȸ������
                        HttpContext.Session.Remove("authCode");
                        return Json(JsonResultString.GetJsonResultStringConvert(true, "ȸ������ �Ϸ�"));
                    }
                    else
                    {
                        return Json(JsonResultString.GetJsonResultStringConvert(true, "�������"));
                    }
                }
                else
                {
                    throw new UserException("���� ��� �����ϴ�");
                }
            }
            catch (UserException u_ex)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, u_ex.Message));
            }
            catch (Exception ex)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, "ȸ������ ����"));
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
                    ViewBag.Result = "�����Ǿ����ϴ�.";
                }
                else
                {
                    throw new UserException("���� �����Դϴ�.");
                }
            }
            catch (UserException u_ex)
            {
                ViewBag.Result = u_ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Result = "���� �����Դϴ�.";
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
                    throw new UserException("�������� �ʴ� ȸ���Դϴ�.");
                }

                string randomPassword = CommonGeneratePasswordBiz.CreateRandomPassword(10);
                member.NewEncPassword = CommonSHA256GenerateBiz.EncryptionSHA256String(randomPassword);
                memberBiz.UpdateMemberTempPassword(member.Email, member.NewEncPassword); //�н����� ����

                StringBuilder mailMessage = new StringBuilder();
                mailMessage.AppendFormat("<meta http-equiv='Content-Type' content='text/html; charset = utf-8'>");
                mailMessage.AppendFormat("�ȳ��ϼ���. {0}�� <br />", memberList[0].Name);
                mailMessage.AppendFormat("�Ʒ��� �ӽú�й�ȣ�� �߱��ص�Ƚ��ϴ�.<br />");
                mailMessage.AppendFormat("{0}<br />", randomPassword);
                mailMessage.AppendFormat("�����մϴ�.");

                await sendMailBiz.SendEmailAsync(member.Email, "", "[PartnerAdmin]�ӽ� ��й�ȣ �߱�", mailMessage.ToString());

                return Json(JsonResultString.GetJsonResultStringConvert(true, "�Է��Ͻ� ���̵� �̸��Ϸ� �ӽú�й�ȣ�� �߱޵Ǿ����ϴ�."));
            }
            catch (UserException u_ex)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, u_ex.Message));
            }
            catch (Exception ex)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, "��й�ȣ �߱� ����"));
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
                    throw new UserException("��й�ȣ�� Ȯ�� �� �ּ���.");
                }

                int result = memberBiz.UpdateMemberPassword(member.Email, member.EncPassword, member.NewEncPassword); //�н����� ����

                if(result != 1)
                {
                    throw new UserException("��й�ȣ ���� �� ����.");
                }

                return Json(JsonResultString.GetJsonResultStringConvert(true, "��й�ȣ�� ���� �Ǿ����ϴ�."));
            }
            catch (UserException u_ex)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, u_ex.Message));
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return Json(JsonResultString.GetJsonResultStringConvert(false, "��й�ȣ ���� ����"));
            }
        }

        public IActionResult MemberInfo()
        {
            return View();
        }
    }
}