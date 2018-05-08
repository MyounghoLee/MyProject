using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using biz.CommonBiz;

namespace biz.CommonBiz
{
    public class CommonValidationCheckBiz
    {
        private string _emailValid = CommonConfigurationBuilderBiz.Configuration.GetSection("valid:email").Value;

        public bool IsEmailCheck(string email)
        {
            bool isResult = true;

            if (email.IndexOf('@') > -1)
            {
                //Validate email format
                string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                       @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                          @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex re = new Regex(emailRegex);
                if (!re.IsMatch(email))
                {
                    isResult = false;
                }
            }
            else
            {
                isResult = false;
            }

            return isResult;
        }

        public bool IsContectEmailCheck(string email)
        {
            bool isResult = true;

            if (IsEmailCheck(email))
            {
                if(!email.ToLower().Contains(_emailValid.ToLower()))
                {
                    isResult = false;
                }
            }
            else
            {
                isResult = false;
            }

            return isResult;
        }
    }
}
