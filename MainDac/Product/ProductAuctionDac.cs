using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace dac.MainDac.Product
{
    public class ProductAuctionDac : BaseMainDac, IMainDac
    {
        public override List<T> SelectProductnfo<T>(string partnerId)
        {
            using (var connection = new MySqlConnection(MysqlConnection))
            {
                connection.Open();
                List<T> list = null;
                var param = new DynamicParameters();
                param.Add("PartnerID", partnerId, DbType.String, ParameterDirection.Input, 50);

                list = connection.Query<T>("SPP_Select_ProductInfo", param, commandType: CommandType.StoredProcedure).AsList();

                return list;
            }
        }
    }
}
