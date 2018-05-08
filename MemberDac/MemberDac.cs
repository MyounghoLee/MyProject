using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace MemberDac
{
    public class MemberDac
    {
        public override List<T> SelectMembertnfo<T>(string partnerId)
        {
            using (var connection = new MySqlConnection(GetDbConnectionString()))
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
