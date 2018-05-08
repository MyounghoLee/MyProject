using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerAdmin.Models.Order
{
    public class OrderM
    {
        public string PartnerId
        {
            get;set;
        }

        public DateTime Ins_Date
        {
            get;set;
        }

        public int Cnt
        {
            get;set;
        }

        public dynamic Values
        {
            get;set;
        }
    }
}
