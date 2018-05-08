using System;
using System.Collections.Generic;
using System.Text;

namespace biz.MainBiz.Order
{
    public interface IOrderBiz
    {
        void SyncOrder(string partnerId, string orderId);
    }
}
