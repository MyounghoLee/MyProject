using System;

namespace biz.MemberBiz
{
    public class MemberBiz
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
