using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using biz.CommonBiz;

namespace dac.MainDac
{
    public class BaseMainDac :IMainDac
    {
        protected static string MysqlConnection;

        public BaseMainDac()
        {
            Init();
        }

        private void Init()
        {
            MysqlConnection = CommonConfigurationBuilderBiz.Configuration.GetSection("mySqlConnectionString:connectionString").Value;
        }

        public virtual List<T> SelectProductnfo<T>(string partnerId)
        {
            List<T> list = null;
            using (CommonSSHClientConnectBiz sshClientConnect = new CommonSSHClientConnectBiz())
            {
                using (var connection = new MySqlConnection(MysqlConnection))
                {
                    connection.Open();
                    var param = new DynamicParameters();
                    param.Add("p_partnerId", partnerId, DbType.String, ParameterDirection.Input, 50);

                    list = connection.Query<T>("SPP_Select_ProductInfo", param, commandType: CommandType.StoredProcedure).AsList();

                    return list;
                }
            }
        }

        public virtual List<T> SelectProductTarget<T>(string partnerId, string goodId, int currentPageNo, int limitPageNo)
        {
            List<T> list = null;
            using (CommonSSHClientConnectBiz sshClientConnect = new CommonSSHClientConnectBiz())
            {
                using (var connection = new MySqlConnection(MysqlConnection))
                {
                    connection.Open();
                    var param = new DynamicParameters();
                    param.Add("p_partnerId", partnerId, DbType.String, ParameterDirection.Input, 50);
                    param.Add("p_goodId", goodId, DbType.String, ParameterDirection.Input, 50);
                    param.Add("p_currentPageNo", currentPageNo, DbType.Int32, ParameterDirection.Input, 4);
                    param.Add("p_limitPageNo", limitPageNo, DbType.Int32, ParameterDirection.Input, 4);

                    list = connection.Query<T>("SP_PartnerAdmin_Select_ProductTarget", param, commandType: CommandType.StoredProcedure).AsList();

                    return list;
                }
            }
        }
    }
}
