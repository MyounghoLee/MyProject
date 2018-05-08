using System;
using System.Collections.Generic;
using System.Text;

namespace biz.HttpHandleBiz.Auction
{
    public class HandleAuctionHostOD : IHandleAuctionHost
    {
        public string DevID { get { return "halfclub"; } }
        public string AppID { get { return "outdous"; } }
        public string AppPassword { get { return "strategy8795"; } }
        public string MemberID { get { return "outdous"; } }
        public string PartnerID { get { return "u1_auction"; } }

        public string Tic
        {
            get
            {
                //발급일자 : 2013-09-16일 오전 11시
                //종료일자 : 2013-12-14일 오전 11시
                //return "dwLL5i0zlv97DwXbcHx2a8ZQgIdk3Lx3/szyM7oIruFNAm/R0DMt4kbdeGxv1ZoUe9rgz6EDOxy+ydBw+KnzpZp/Dni11DNlljn4SqSW8X3/c5wxLF0/p8vRrawnP6LWMS8ccP+KsN5t5Jlqj3DJ+p1Cd7YPUJPthMKA/I/QtxzeOFDCJUSqbiUUXonfnEfdggCxyruZHXDXQ/rTZSC5MxA=";

                //발급일자 : 2013-12-13일 오전 11시
                //종료일자 : 무기한
                return "d8qvFZCIjbwr+iUwvZXC2vQ+om7bMDCg21WOVjv8d2PiI819EJ6bvtwTxWBRRMrtas+BPlY2e03Wsm920RUMEBhCHv55gCqtwuZtgUA63mvx1E/ToRxaFJJOTKLqjoNVq0upASSBWyXmHcHXnbf47W6Pd/I3GtCQH5nQYIyvBh/IQp1vWnq3V4F8P9DeEU9DVegbsQUr2MyYd+/b7+zpJiU=";
            }
        }
    }
}
