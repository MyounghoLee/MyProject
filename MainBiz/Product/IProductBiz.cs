using models.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace biz.MainBiz.Product
{
    public interface IProductBiz
    {
        List<ProductInfo> GetProductInfo(string partnerId);
    }
}
