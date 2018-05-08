using System;

namespace models.Member
{
    public class Member
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Grade { get; set; }
        public DateTime UptDate { get; set; }
        public DateTime InsDate { get; set; }
        public bool IsUse { get; set; }
    }
}
