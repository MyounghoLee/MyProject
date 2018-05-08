using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using biz.CommonBiz;
using dac.MainDac;
using Dapper;
using MySql.Data.MySqlClient;

namespace MainDac.Chart
{
    public class OrderChartDac : BaseMainDac, IMainDac
    {
        public List<T> GetOrderChartList<T>(string domCd, int beforeDay)
        {
            List<T> list = null;
            using (CommonSSHClientConnectBiz sshClientConnect = new CommonSSHClientConnectBiz())
            {
                using (var connection = new MySqlConnection(MysqlConnection))
                {
                    connection.Open();
                    var param = new DynamicParameters();
                    param.Add("p_domCd", domCd, DbType.String, ParameterDirection.Input, 2);
                    param.Add("p_day", beforeDay, DbType.Int32, ParameterDirection.Input, 4);

                    list = connection.Query<T>("SP_PartnerAdmin_Select_ChartOrderByOrderM", param, commandType: CommandType.StoredProcedure).AsList();
                }
            }

            return list;
        }
    }
}
