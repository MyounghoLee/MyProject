using System;
using System.Collections.Generic;
using System.Text;

namespace biz.MainBiz.Order
{
    public class OrderAuctionBiz : BaseMainBiz, IOrderBiz
    {
        public void SyncOrder(string partnerId, string orderId)
        {
            Console.Write("sync order");
        }
    }
}
