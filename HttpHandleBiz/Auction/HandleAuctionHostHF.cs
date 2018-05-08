using System;
using System.Collections.Generic;
using System.Text;

namespace biz.HttpHandleBiz.Auction
{
    public class HandleAuctionHostHF : IHandleAuctionHost
    {
        public string DevID { get { return "halfclub"; } }
        public string AppID { get { return "halfclub"; } }
        public string AppPassword { get { return "gkvmzmffjq"; } }
        public string MemberID { get { return "half1234"; } }
        public string PartnerID { get { return "auction2"; } }

        public string Tic
        {
            get
            {
                //발급일자 : 2013-06-19일 오후 5시
                //종료일자 : 2013-09-19일 오후 5시
                //return "dxd0V4BbJDpPOebiGu7NIQZhU9rZk/tYrjXnkhCFJd1plVo78U5xJuWqimuaOvMm4SjiSgw+GYhLvvewuGS2ZewbtzEUE//URm9e+j7PSSx15/VSE4atGA71Jl/M10J8K4wZWdMQIX01MwriDRqJnRVldBzN4VkzNLdqvQI0ffyQNxEIPWG2ZkQkS4ljWCy5CPfte0r2AGiMSQBwy+CbRqY=";

                ////발급일자 : 2013-09-13일 오후 2시
                ////종료일자 : 2013-12-13일 오후 2시
                //return "d3sCylEvTgzKvl9Ew8kPF3can3XzFZ2wWQVdhi8J0I2UwxPnJpfBFhSQjn/o4EqfZtIMVOoXggEGpikuWI7X7iY8IaKfMQOAKhhZHMcajsBJYVnnrbHxSgw1RMOjzrfuNMryJkZk6w2XAgPysHSqimv/3SME321gU56b+5hDW8Y/ID9gwQJf/8/3/tLIhcAssDnEURPK2kmeYEDsLcH/Mjk=";

                //발급일자 : 2013-09-16일 오전 11시
                //종료일자 : 2013-12-14일 오전 11시
                //return "d4g9eczVWNbqWG2wubcx36dL6f+MjmdMKMJ7h2gP87VRLmmDUlFgnq3TUoVdlnq3gpjomwfw34lRBRHOKbq8I+vneZLWZlgKuflMvRUP4vDGrjpTWm7rpdFiL6MteMKss7unu7cNhe61vAHeQRa+lTuV1isIguzVFyqPG+Sx/7Aaa/NDeS2doa8X5NY3y8Up4AmNnKJ3k42wfA3QABlYEUc=";

                //발급일자 : 2013-12-13일 오전 11시
                //종료일자 : 무기한
                return "d7ph8XwTwFdKLnNL7gJO0fiRr9L31zikCn4MLhfflThi21J2ReQaYmnBFScPkS7jSt0EuTTINQsUWxHahkYiaRapGDIU/JlR7t5Y7V5QRYwADh1mzGexiOW03JjypXo7xa8IuBh80KQ7zodpz/d9zeAeigcyYQ8oS6FRXapg+hsDYjHnAn3gbG0IR6FmHUHWAV0uxgwxeFm424QF0/JvplQ=";



            }
        }
    }
}
