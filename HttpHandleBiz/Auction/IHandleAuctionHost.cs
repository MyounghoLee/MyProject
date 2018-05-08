using System;
using System.Collections.Generic;
using System.Text;

namespace biz.HttpHandleBiz.Auction
{
    public interface IHandleAuctionHost
    {
        string MemberID { get; }
        string Tic { get; }
        string DevID { get; }
        string AppID { get; }
        string AppPassword { get; }
        string PartnerID { get; }
    }
}
