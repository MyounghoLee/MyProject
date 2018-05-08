using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using biz.CommonBiz;
using dac.MainDac;
using Dapper;
using MySql.Data.MySqlClient;

namespace dac.MainDac.PartnerAdminMenu
{
    public class PartnerAdminMenuDac : BaseMainDac
    {
        public List<T> GetetPartnerAdminMenuList<T>()
        {
            List<T> list = null;
            using (CommonSSHClientConnectBiz sshClientConnect = new CommonSSHClientConnectBiz())
            {
                using (var connection = new MySqlConnection(MysqlConnection))
                {
                    connection.Open();
                    list = connection.Query<T>("SP_PartnerAdmin_Select_PartnerAdminMenu", commandType: CommandType.StoredProcedure).AsList();
                }
            }

            return list;
        }
    }
}
