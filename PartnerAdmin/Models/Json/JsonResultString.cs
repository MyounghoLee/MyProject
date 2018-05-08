using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PartnerAdmin.Models.Json
{
    public static class JsonResultString
    {
        public static string GetJsonResultStringConvert(bool isSuccess, string message)
        {
            var result = new
            {
                Success = isSuccess,
                Message = message
            };

            return JsonConvert.SerializeObject(result);
        }

        public static string GetJsonResultStringConvert(bool isSuccess, dynamic list)
        {
            var result = new
            {
                Success = isSuccess,
                List = list
            };

            return JsonConvert.SerializeObject(result);
        }

        public static string GetJsonResultStringConvert<T>(bool isSuccess, List<T> list)
        {
            var result = new
            {
                Success = isSuccess,
                List = list
            };

            return JsonConvert.SerializeObject(result);
        }
    }
}
