using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerAdmin.Models.PartnerAdminMenu
{
    public class PartnerAdminMenu
    {
        public int Menu_Id;
        public int Menu_ParentId;
        public string Menu_Name;
        public string Menu_Url;
        public int Menu_Sort;
        public bool Menu_IsUse;
        public DateTime UptDate;
        public DateTime InsDate;
        public string Menu_Grade;
    }
}

