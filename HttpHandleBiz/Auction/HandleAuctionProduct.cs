using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AuctionShoppingServiceSoap;

namespace biz.HttpHandleBiz.Auction
{
    public class HandleAuctionProduct : BaseHandleAuction
    {
        public HandleAuctionProduct(string partnerId) : base(partnerId)
        {
            
        }

        #region API 호출
        // 상품 등록 호출
        public async Task<AddItemResponseT> API_AddItemAsync(AddItemRequestT req)
        {
            AddItemResponseT res = new AddItemResponseT();

            try
            {
                req.MemberTicket = new MemberTicketT();
                req.MemberTicket.Ticket = Host.Tic;
                req.InputChannel = Host.PartnerID;
                res = await ShopClient.AddItemAsync(req);
                
            }
            catch (WebException webex)
            {
                webex.ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return res;
        }

        // 상품고시 등록 호출
        public async Task<AddOfficialNoticeResponseT> API_AddOfficialNoticeAsync(AddOfficialNoticeRequestT Req)
        {
            AddOfficialNoticeResponseT res = await ShopClient.AddOfficialNoticeAsync(Req);
            return res;
        }

        // 옵션 등록 호출
        public async Task<ReviseStockResponseT> API_ReviseItemStockAsync(ReviseStockRequestT stockRequest)
        {
            ReviseStockResponseT res = await ShopClient.ReviseItemStockAsync(stockRequest);
            return res;
        }

        public async Task<ReviseStockResponseT> API_ReviseItemAsync(ReviseStockRequestT itemRequest)
        {
            ReviseStockResponseT res = await ShopClient.ReviseItemStockAsync(itemRequest);
            return res;
        }

        // 옥션브랜드관 호출
        public async Task<ReviseItemResponseT> API_ReviseItemForBrandShopAsync(ReviseItemForBrandShopRequestT brandShopRequest)
        {
            ReviseItemResponseT res = await ShopClient.ReviseItemForBrandShopAsync(brandShopRequest);
            return res;
        }

        // 판매정보 등록 호출
        public async Task<ReviseItemSellingResponseT> API_ReviseItemSellingAsync(ReviseItemSellingRequestT sellingRequest)
        {
            ReviseItemSellingResponseT res = await ShopClient.ReviseItemSellingAsync(sellingRequest);
            return res;
        }

        // 옥션상품 상태 호출
        public async Task<string> API_ViewItemStatusAsync(string ItemID)
        {
            ViewItemRequestT req = new ViewItemRequestT();
            req.ItemID = ItemID;
            string res = await ShopClient.ViewItemStatusAsync(req);

            return res;
        }

        /// <summary>
        /// 옥션 상품정보 조회
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public async Task<ViewItemResponseT> API_ItemInfoAsync(ViewItemRequestT itemRequest)
        {
            ViewItemResponseT res = await ShopClient.ViewItemAsync(itemRequest);
            return res;
        }

        #endregion

    }
}
