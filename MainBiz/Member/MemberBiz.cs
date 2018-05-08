using System;
using System.Collections.Generic;
using System.Text;
using dac.MainDac.Member;

namespace biz.MainBiz.Member
{
    public class MemberBiz
    {
        private MemberDac _memberDac = null;

        public MemberBiz()
        {
            _memberDac = new MemberDac();
        }

        /// <summary>
        /// 아이디로 조회
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<T> GetMemberInfo<T>(string id)
        {
            try
            {
                List<T> memberList = _memberDac.GetMemberList<T>(id);

                return memberList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 비밀번호 조회
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public List<T> GetMemberInfo<T>(string email, string encPassword)
        {
            try
            {
                List<T> memberList = _memberDac.GetMemberList<T>(email, encPassword);

                return memberList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 회원가입
        /// </summary>
        /// <param name="email"></param>
        /// <param name="encPassword"></param>
        /// <param name="name"></param>
        public void RegisterMember(string email, string encPassword, string name)
        {
            try
            {
                _memberDac.InsertMemberinfo(email, encPassword, name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 임시패스워드 발급
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newEncPassword"></param>
        /// <returns></returns>
        public int UpdateMemberTempPassword(string email, string newEncPassword)
        {
            try
            {
                return _memberDac.UpdateMemberTempPassword(email, newEncPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        /// <summary>
        /// 패스워드 변경
        /// </summary>
        /// <param name="email"></param>
        /// <param name="encPassword"></param>
        /// <param name="newEncPassword"></param>
        public int UpdateMemberPassword(string email, string encPassword, string newEncPassword)
        {
            try
            {
                return _memberDac.UpdateMemberPassword(email, encPassword, newEncPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 멤버 인증 대상 리스트
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="email"></param>
        /// <param name="activationcode"></param>
        /// <returns></returns>
        public List<T> GetAuthMemberList<T>(string email, string authcode)
        {
            try
            {
                List<T> memberActivationList  = _memberDac.SelectAuthMember<T>(email, authcode);

                return memberActivationList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 멤버 인증 대상 리스트
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="email"></param>
        /// <param name="activationcode"></param>
        /// <returns></returns>
        public List<T> GetAuthMemberList<T>(string email)
        {
            try
            {
                List<T> memberActivationList = _memberDac.SelectAuthMember<T>(email);

                return memberActivationList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 멤버 인증 대상 등록
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="email"></param>
        /// <param name="activationcode"></param>
        /// <returns></returns>
        public void RegisterAuthMember(string email, string authCode)
        {
            try
            {
                _memberDac.InsertAuthMember(email, authCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 멤버 인증 대상 업데이트
        /// </summary>
        /// <param name="email"></param>
        /// <param name="activationcode"></param>
        /// <returns></returns>
        public int ChangeAuthMember(string email, string authCode)
        {
            try
            {
                int result = _memberDac.UpdateAuthMember(email, authCode);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
