
using biz.MainBiz.Order;
using biz.MainBiz.Product;
using System;
using System.Collections.Generic;
using System.Text;
using models.Product;
using dac.MainDac;

namespace biz.MainBiz
{
    public class BaseMainBiz : IMainBiz
    {
        BaseMainDac _mainDac = new BaseMainDac();

        public static IProductBiz GetProductInstance(string partnerId)
        {
            switch (partnerId.ToUpper())
            {
                case "AUCTION":
                case "AUCTION2":
                case "O2_AUCTION_DB":
                case "U1_AUCTION":
                    return new ProductAuctionBiz();
                default:
                    return null;
            }
        }

        public static IOrderBiz GetOrderInstance(string partnerId)
        {
            switch (partnerId.ToUpper())
            {
                case "AUCTION":
                case "AUCTION2":
                case "O2_AUCTION_DB":
                case "U1_AUCTION":
                    return new OrderAuctionBiz();
                default:
                    return null;
            }
        }

        public virtual List<ProductInfo> GetProductInfo(string partnerId)
        {
            try
            {
                List<ProductInfo> productInfo = _mainDac.SelectProductnfo<ProductInfo>(partnerId);

                return productInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual List<ProductTarget> GetProductTargetList(string partnerId, string goodId, int currentPageNo, int limitPageNo)
        {
            try
            {
                List<ProductTarget> productTargetList = _mainDac.SelectProductTarget<ProductTarget>(partnerId, goodId, currentPageNo, limitPageNo);

                return productTargetList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
