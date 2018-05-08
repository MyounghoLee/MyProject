using System;
using System.Collections.Generic;
using System.Text;

namespace biz.ExceptionBiz
{
    public class UserException :Exception
    {
        public UserException() { }

        public UserException(string message) : base(message) {
        }
        
    }
}
