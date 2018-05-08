using System;

namespace PartnerAdmin.Models.Member
{
    public class AuthMember
    {
        public string Email { get; set; }

        public string AuthCode { get; set; }

        public bool IsAuth { get; set; }

        public DateTime UptDate { get; set; }

        public DateTime InsDate { get; set; }
    }
}
