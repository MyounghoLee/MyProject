using System;
using System.Collections.Generic;
using System.Text;

namespace biz.HttpHandleBiz.Auction
{
    public class BaseHandleAuction
    {
        public IHandleAuctionHost Host = null;
        protected AuctionShoppingServiceSoap.MemberT ShopMember; // ShoppingService 판매자 객체
        protected AuctionShoppingServiceSoap.EncryptedTicketHeader ShopAuth; // ShoppingService 헤더 객체
        protected AuctionShoppingServiceSoap.ShoppingServiceSoapClient ShopClient; // ShoppingService 호출 객체
        protected AuctionAuctionServiceSoap.EncryptedTicketHeader AuctionAuth;
        protected AuctionAuctionServiceSoap.AuctionServiceSoapClient AuctionClient;
        protected AuctionMainService.MainServiceSoapClient MainClient;    // MainService 호출 객체
        protected AuctionMainService.EncryptedTicketHeader AuctionAuth_Main;

        public BaseHandleAuction(string partnerId)
        {
            Host = GetHandleAuctionHost(partnerId);
            ShopMember = new AuctionShoppingServiceSoap.MemberT();
            ShopMember.MemberID = Host.MemberID;
            ShopAuth = new AuctionShoppingServiceSoap.EncryptedTicketHeader();
            
            ShopAuth.Value = Host.Tic;   // 인증티켓
            ShopClient = new AuctionShoppingServiceSoap.ShoppingServiceSoapClient(AuctionShoppingServiceSoap.ShoppingServiceSoapClient.EndpointConfiguration.ShoppingServiceSoap);

            AuctionAuth = new AuctionAuctionServiceSoap.EncryptedTicketHeader();
            AuctionAuth.Value = Host.Tic;
            AuctionClient = new AuctionAuctionServiceSoap.AuctionServiceSoapClient(AuctionAuctionServiceSoap.AuctionServiceSoapClient.EndpointConfiguration.AuctionServiceSoap);

            MainClient = new AuctionMainService.MainServiceSoapClient(AuctionMainService.MainServiceSoapClient.EndpointConfiguration.MainServiceSoap);

            AuctionAuth_Main = new AuctionMainService.EncryptedTicketHeader();
            AuctionAuth_Main.Value = Host.Tic;
        }

        public IHandleAuctionHost GetHandleAuctionHost(string partnerId)
        {
            switch (partnerId.ToLower())
            {
                case "auction2":
                case "auction":
                    return new HandleAuctionHostHF();
                case "b_auction":
                case "b_auction2":
                    return new HandleAuctionHostBR();
                case "o2_auction_db":
                case "o1_auction":
                    return new HandleAuctionHostGA();
                case "u1_auction":
                    return new HandleAuctionHostOD();
                default:
                    throw new Exception("Host Error");
            }
        }
    }
}
