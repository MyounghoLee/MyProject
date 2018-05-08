using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using biz.CommonBiz;
using dac.MainDac;
using Dapper;
using MySql.Data.MySqlClient;
using Renci.SshNet;

namespace dac.MainDac.Member
{
    public class MemberDac : BaseMainDac, IMainDac
    {
        public List<T> GetMemberList<T>(string email)
        {
            List<T> list = null;
            using (CommonSSHClientConnectBiz sshClientConnect = new CommonSSHClientConnectBiz())
            {
                using (var connection = new MySqlConnection(MysqlConnection))
                {
                    connection.Open();
                    var param = new DynamicParameters();
                    param.Add("p_Email", email, DbType.String, ParameterDirection.Input, 50);

                    list = connection.Query<T>("SP_PartnerAdmin_Select_Member_ByEmail", param, commandType: CommandType.StoredProcedure).AsList();
                }
            }

            return list;
        }
            
        public List<T> GetMemberList<T>(string email, string encPassword)
        {
            List<T> list = null;
            using (CommonSSHClientConnectBiz sshClientConnect = new CommonSSHClientConnectBiz())
            {
                using (var connection = new MySqlConnection(MysqlConnection))
                {
                    connection.Open();
                    var param = new DynamicParameters();
                    param.Add("p_Email", email, DbType.String, ParameterDirection.Input, 50);
                    param.Add("p_Password", encPassword, DbType.String, ParameterDirection.Input, 200);

                    list = connection.Query<T>("SP_PartnerAdmin_Select_Member_ByPassword", param, commandType: CommandType.StoredProcedure).AsList();
                }
            }

            return list;
        }

        /// <summary>
        /// 회원가입
        /// </summary>
        /// <param name="email"></param>
        /// <param name="encPassword"></param>
        /// <param name="name"></param>
        public void InsertMemberinfo(string email, string encPassword, string name)
        {
            using (CommonSSHClientConnectBiz sshClientConnect = new CommonSSHClientConnectBiz())
            {
                using (var connection = new MySqlConnection(MysqlConnection))
                {
                    connection.Open();
                    var param = new DynamicParameters();
                    param.Add("p_Email", email, DbType.String, ParameterDirection.Input, 50);
                    param.Add("p_Password", encPassword, DbType.String, ParameterDirection.Input, 200);
                    param.Add("p_Name", name, DbType.String, ParameterDirection.Input, 20);

                    connection.Execute("SP_PartnerAdmin_Insert_Member", param, commandType: CommandType.StoredProcedure);
                }
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
            using (CommonSSHClientConnectBiz sshClientConnect = new CommonSSHClientConnectBiz())
            {
                using (var connection = new MySqlConnection(MysqlConnection))
                {
                    connection.Open();
                    var param = new DynamicParameters();
                    param.Add("p_Email", email, DbType.String, ParameterDirection.Input, 50);
                    param.Add("p_NewPassword", newEncPassword, DbType.String, ParameterDirection.Input, 200);

                    return connection.Execute("SP_PartnerAdmin_Update_Member_TempPassword", param, commandType: CommandType.StoredProcedure);
                }
            }
        }

        /// <summary>
        /// 패스워드 변경
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="newPassword"></param>
        public int UpdateMemberPassword(string email, string encPassword, string newEncPassword)
        {
            using (CommonSSHClientConnectBiz sshClientConnect = new CommonSSHClientConnectBiz())
            {
                using (var connection = new MySqlConnection(MysqlConnection))
                {
                    connection.Open();
                    var param = new DynamicParameters();
                    param.Add("p_Email", email, DbType.String, ParameterDirection.Input, 50);
                    param.Add("p_Password", encPassword, DbType.String, ParameterDirection.Input, 200);
                    param.Add("p_NewPassword", newEncPassword, DbType.String, ParameterDirection.Input, 200);

                    return connection.Execute("SP_PartnerAdmin_Update_Member_Password", param, commandType: CommandType.StoredProcedure);
                }
            }
        }

        /// <summary>
        /// 멤버 인증 인서트
        /// </summary>
        /// <param name="email"></param>
        /// <param name="authCode"></param>
        public void InsertAuthMember(string email, string authCode)
        {
            using (CommonSSHClientConnectBiz sshClientConnect = new CommonSSHClientConnectBiz())
            {
                using (var connection = new MySqlConnection(MysqlConnection))
                {
                    connection.Open();
                    var param = new DynamicParameters();
                    param.Add("p_Email", email, DbType.String, ParameterDirection.Input, 50);
                    param.Add("p_AuthCode", authCode, DbType.String, ParameterDirection.Input, 200);

                    connection.Execute("SP_PartnerAdmin_Insert_AuthMember", param, commandType: CommandType.StoredProcedure);
                }
            }
        }

        /// <summary>
        /// 멤버 이메일 인증 대상 리스트
        /// </summary>
        /// <param name="email"></param>
        /// <param name="authcode"></param>
        public List<T> SelectAuthMember<T>(string email, string authCode)
        {
            List<T> list = null;
            using (CommonSSHClientConnectBiz sshClientConnect = new CommonSSHClientConnectBiz())
            {
                using (var connection = new MySqlConnection(MysqlConnection))
                {
                    connection.Open();
                    var param = new DynamicParameters();
                    param.Add("p_Email", email, DbType.String, ParameterDirection.Input, 50);
                    param.Add("p_AuthCode", authCode, DbType.String, ParameterDirection.Input, 200);

                    list = connection.Query<T>("SP_PartnerAdmin_Select_AuthMember_byAuthCode", param, commandType: CommandType.StoredProcedure).AsList();
                }
            }

            return list;
        }

        /// <summary>
        /// 멤버 이메일 인증 대상 리스트
        /// </summary>
        /// <param name="email"></param>
        public List<T> SelectAuthMember<T>(string email)
        {
            List<T> list = null;
            using (CommonSSHClientConnectBiz sshClientConnect = new CommonSSHClientConnectBiz())
            {
                using (var connection = new MySqlConnection(MysqlConnection))
                {
                    connection.Open();
                    var param = new DynamicParameters();
                    param.Add("p_Email", email, DbType.String, ParameterDirection.Input, 50);

                    list = connection.Query<T>("SP_PartnerAdmin_Select_AuthMember_byEmail", param, commandType: CommandType.StoredProcedure).AsList();
                }
            }

            return list;
        }

        /// <summary>
        /// 멤버 인증 완료
        /// </summary>
        /// <param name="email"></param>
        /// <param name="authCode"></param>
        public int UpdateAuthMember(string email, string authCode)
        {
            using (CommonSSHClientConnectBiz sshClientConnect = new CommonSSHClientConnectBiz())
            {
                using (var connection = new MySqlConnection(MysqlConnection))
                {
                    connection.Open();
                    var param = new DynamicParameters();
                    param.Add("p_Email", email, DbType.String, ParameterDirection.Input, 50);
                    param.Add("p_AuthCode", authCode, DbType.String, ParameterDirection.Input, 200);

                    return connection.Execute("SP_PartnerAdmin_Update_AuthMember", param, commandType: CommandType.StoredProcedure);
                }
            }
        }
    }
}
