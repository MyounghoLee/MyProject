using biz.MainBiz.Product;
using models.Product;
using System;
using System.Collections.Generic;
using biz.MainBiz;
using biz.CommonBiz;
using biz.HttpHandleBiz.Auction;
using AuctionShoppingServiceSoap;

namespace ServiceAuction
{
    class Program
    {
        /// <summary>
        /// main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            CommonConfigurationBuilderBiz.SetConfgurationBuilder();

            IProductBiz iProductBiz = BaseMainBiz.GetProductInstance("AUCTION");
            //List<ProductInfo> productInfoList = iProductBiz.GetProductInfo("HF_AUCTION");

            HandleAuctionProduct handleAuctionPrd = new HandleAuctionProduct("AUCTION");
            Console.WriteLine(handleAuctionPrd.Host.AppID);
            Console.WriteLine(handleAuctionPrd.Host.AppPassword);
            Console.WriteLine(handleAuctionPrd.Host.DevID);
            Console.WriteLine(handleAuctionPrd.Host.MemberID);
            Console.WriteLine(handleAuctionPrd.Host.PartnerID);
            AddItemRequestT req = new AddItemRequestT();
            dynamic res =  handleAuctionPrd.API_AddItemAsync(req);

            Console.WriteLine(res);
            Console.ReadLine();
        }
    }
}