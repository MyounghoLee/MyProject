using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using biz.CommonBiz;

namespace biz.CommonBiz
{
    public static class CommonSHA256GenerateBiz
    {
        private static string _macAlgorithm = CommonConfigurationBuilderBiz.Configuration.GetSection("crypt:macAlgorithm").Value;
        private static string _encodingMode = CommonConfigurationBuilderBiz.Configuration.GetSection("crypt:encodingMode").Value;
        private static string _setMacKeyString = CommonConfigurationBuilderBiz.Configuration.GetSection("crypt:setMacKeyString").Value;
        private static string _hashAlgorithm = CommonConfigurationBuilderBiz.Configuration.GetSection("crypt:hashAlgorithm").Value;

        public static string EncryptionSHA256String(string inputString)
        {
            string result = string.Empty;
            var key = Encoding.UTF8.GetBytes(_setMacKeyString.ToUpper());

            using (var hmac = new HMACSHA256(key))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(inputString));
                result = Convert.ToBase64String(hash);
            }

            return result;
        }
    }
}
