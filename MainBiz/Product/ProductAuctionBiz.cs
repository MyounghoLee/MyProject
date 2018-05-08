using dac.MainDac.Product;
using models.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace biz.MainBiz.Product
{
    public class ProductAuctionBiz : BaseMainBiz, IProductBiz
    {
        private ProductAuctionDac _productAuctionDac = null;

        public ProductAuctionBiz()
        {
            _productAuctionDac = new ProductAuctionDac();
        }

        public override List<ProductInfo> GetProductInfo(string partnerId)
        {
            try
            {
                List<ProductInfo> productInfo = _productAuctionDac.SelectProductnfo<ProductInfo>(partnerId);

                return productInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
