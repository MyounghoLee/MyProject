using System;
using System.Collections.Generic;
using System.Text;

namespace biz.HttpHandleBiz.Auction
{
    public class HandleAuctionHostGA : IHandleAuctionHost
    {
        public string DevID { get { return "halfclub"; } }
        public string AppID { get { return "half1234"; } }
        public string AppPassword { get { return "gkvmzmffjq"; } }
        public string MemberID { get { return "half1234"; } }
        public string PartnerID { get { return "o2_auction_db"; } }
        //public string PartnerID { get { return "o1_auction"; } }
        public int LimitQty { get { return 5; } }
        public string CSTel { get { return ""; } }

        public string Tic
        {
            get
            {
                //발급일자 : 2013-09-16일 오전 11시
                //종료일자 : 2013-12-14일 오전 11시
                //return "dzQm4CHVEa50Mil9F22uXWV8Wy37j3WafyWLjLEOyab4y//BL093jAV1vO7/Nc8IErLC0qUbE8brmv/dOoqFOj1S/50/tirwdvDEVaNbgIPkgLTFUCHL1d49dHsaVW7MpG/gw3uKqrkBoTNSR2vE1Bh2iBJQ4/KsHf4voh84vXDgLtGqME64fdrKQj3KRMo9MI1CO6yezyxSemKZo6VePLQ=";

                //발급일자 : 2013-12-13일 오전 11시
                //종료일자 : 무기한
                return "d925lNGMWS5bUAIg4nm73X+EILtS5zBJJAUMvgfnnNs3N0OG0ayQddbkkUigR+TRLHCFVdY+vPTu7Sd1OsTXPf5YLVUdaGzBbP61FLxvkBXOc03cwwlg26j44GhCk1m4DIPJIuLa5Zddc0pkb+AowTv1zkljjwuNmAtBqWmiJiDyqRZ91kpQZtZVwlVKuAsqRXTqtCJ6XTOfcKDgES8a63o=";
            }
        }
    }
}
