using System;
using System.Collections.Generic;
using System.Text;
using AuctionAuctionServiceSoap;
using System.Threading.Tasks;
using AuctionMainService;
using System.Text.RegularExpressions;

namespace biz.HttpHandleBiz.Auction
{
    public class HandleAuctionOrder : BaseHandleAuction
    {
        public HandleAuctionOrder(string partnerId) : base(partnerId)
        {
            

        }

        #region 주문연동
        //주문목록 조회
        public async Task<GetShippingAddressListResponse> RequestOrderListAsync()
        {
            GetShippingAddressListRequestT req = new GetShippingAddressListRequestT();
            req = Generate_GetOrderList();
            GetShippingAddressListResponse res = await AuctionClient.GetShippingAddressListAsync(req);

            return res;
        }

        //반품요청 목록 조회
        public async Task<GetReturnListResponse> GetReturnListAsync(DateTime StartDate, DateTime EndDate)
        {
            GetReturnListRequestT req = new GetReturnListRequestT();
            req = Generate_GetReturnList(StartDate, EndDate);

            GetReturnListResponse res = await AuctionClient.GetReturnListAsync(req);

            return res;
        }

        //취소요청 목록 조회
        public async Task<GetCancelApprovalListResponse> GetCancelListAsync(DateTime StartDate, DateTime EndDate)
        {
            GetCancelApprovalRequestT req = new GetCancelApprovalRequestT();
            req = Generate_GetCancelList(StartDate, EndDate);

            GetCancelApprovalListResponse res = await AuctionClient.GetCancelApprovalListAsync(req);

            return res;
        }

        //완료된 주문,반품 목록 조회 - Partner_OrderCancel에 넣는 내용.
        public async Task<GetOrderCanceledListResponse> SyncOrderClaimAsync(DateTime StartDate, DateTime EndDate)
        {
            GetOrderCanceledListRequestT req = new GetOrderCanceledListRequestT();
            req = Generate_GetOrderCanceledList(StartDate, EndDate);

            GetOrderCanceledListResponse res = await AuctionClient.GetOrderCanceledListAsync(req);

            return res;
        }

        //송금완료자료 조회
        public async Task<GetCompletedRemittanceListResponse> SyncSettlementAsync(DateTime StartDate, DateTime EndDate, int PageIndex)
        {
            GetCompletedRemittanceListRequestT req = new GetCompletedRemittanceListRequestT();
            req = Generate_SyncSettlement(StartDate, EndDate, PageIndex);

            GetCompletedRemittanceListResponse res = await AuctionClient.GetCompletedRemittanceListAsync(req);

            return res;
        }

        //정산상세정보조회
        public async Task<GetSettlementDetailResponseT> SyncSettlementDetailAsync(int OrderNo)
        {
            GetSettlementDetailRequestT req = new GetSettlementDetailRequestT();
            req = Generate_SyncSettlementDetail(OrderNo);

            GetSettlementDetailResponseT res = await AuctionClient.GetSettlementDetailAsync(req);

            return res;
        }

        //주문상세정보조회
        public async Task<GetOrderDetailInfoByOrderNoResponseT> GetOrderDetailByOrderNoAsync(string OrderNo)
        {
            GetOrderDetailInfoByOrderNoRequestT req = new GetOrderDetailInfoByOrderNoRequestT();
            req.OrderNo = Convert.ToInt32(OrderNo);
            req.OrderNoSpecified = true;

            GetOrderDetailInfoByOrderNoResponseT res = await MainClient.GetOrderDetailInfoByOrderNoAsync(req);

            return res;
        }

        //주문완료후 주문확인 : 특정 상품 주문을 발주처리
        public async Task ConfirmOrderAsync(int OrderNo)
        {
            AuctionAuctionServiceSoap.ConfirmReceivingOrderRequestT req = new AuctionAuctionServiceSoap.ConfirmReceivingOrderRequestT();
            req.OrderNo = OrderNo;
            req.OrderNoSpecified = true;

            await AuctionClient.ConfirmReceivingOrderAsync(req);

        }

        /// <summary>
        /// DataRow는 core에서 지원안됨.. 
        /// </summary>
        /// <param name="Dr"></param>
        /// <returns></returns>
        public async Task<DoShippingGeneralResponseT> SetDeliveryOrderInfoAsync()/*DataRow Dr*/
        {
            DoShippingGeneralRequestT req = new DoShippingGeneralRequestT();
            //req = Generate_SetDeliveryOrderInfoAsync(Dr);

            DoShippingGeneralResponseT res = new DoShippingGeneralResponseT();
            res = await AuctionClient.DoShippingGeneralAsync(req);

            return res;
        }


        #endregion

        #region API 호출 객체 생성
        private GetShippingAddressListRequestT Generate_GetOrderList()
        {
            GetShippingAddressListRequestT addListReq = new GetShippingAddressListRequestT();
            addListReq.ReceiveType = AuctionAuctionServiceSoap.ShippingAddressListReceiveCode.NotReceiving;
            addListReq.SendType = AuctionAuctionServiceSoap.ShippingAddressListSendCode.NotSending;
            addListReq.DurationTypeSpecified = false;
            addListReq.ReceiveTypeSpecified = true;
            addListReq.SearchTypeSpecified = false;
            addListReq.SendTypeSpecified = true;

            return addListReq;
        }

        private GetReturnListRequestT Generate_GetReturnList(DateTime StartDate, DateTime EndDate)
        {
            GetReturnListRequestT Req = new GetReturnListRequestT();
            Req.SearchDateType = ReturnListSearchDateCode.Request;
            Req.SearchDuration = new AuctionAuctionServiceSoap.DurationT();
            Req.SearchDuration.StartDate = StartDate;
            Req.SearchDuration.EndDate = EndDate;
            Req.SearchFlags = new ReturnStatus[1] { ReturnStatus.Requested };

            Req.SearchDateTypeSpecified = true;
            Req.SearchDuration.StartDateSpecified = true;
            Req.SearchDuration.EndDateSpecified = true;
            Req.SearchTypeSpecified = false;

            return Req;
        }

        private GetCancelApprovalRequestT Generate_GetCancelList(DateTime StartDate, DateTime EndDate)
        {
            GetCancelApprovalRequestT Req = new GetCancelApprovalRequestT();
            Req.SearchDateType = CancelApprovalListSearchDateCode.CancelRequestDate;
            Req.SearchDateTypeSpecified = true;
            Req.FromDate = StartDate.ToString("yyyy-MM-dd");
            Req.ToDate = EndDate.ToString("yyyy-MM-dd");
            Req.SearchType = CancelApprovalListSearchCode.None;
            Req.SearchTypeSpecified = true;
            return Req;
        }

        private GetOrderCanceledListRequestT Generate_GetOrderCanceledList(DateTime StartDate, DateTime EndDate)
        {
            GetOrderCanceledListRequestT Req = new GetOrderCanceledListRequestT();
            Req.SearchDuration = new AuctionAuctionServiceSoap.DurationT();
            Req.SearchDuration.StartDate = StartDate;
            Req.SearchDuration.StartDateSpecified = true;
            Req.SearchDuration.EndDate = EndDate;
            Req.SearchDuration.EndDateSpecified = true;
            Req.SearchType = OrderCanceledListDurationCode.Nothing;

            return Req;
        }

        private GetCompletedRemittanceListRequestT Generate_SyncSettlement(DateTime StartDate, DateTime EndDate, int PageIndex = 1)
        {
            GetCompletedRemittanceListRequestT Req = new GetCompletedRemittanceListRequestT();

            Req.CategoryID = string.Empty;//필수항목.
            Req.DurationType = CompletedRemittanceListDurationCode.RemittanceDate; //송금일기준
            Req.DurationTypeSpecified = true;
            Req.SearchDuration = new AuctionAuctionServiceSoap.DurationT();
            Req.SearchDuration.StartDate = StartDate;
            Req.SearchDuration.StartDateSpecified = true;
            Req.SearchDuration.EndDate = EndDate;
            Req.SearchDuration.EndDateSpecified = true;
            Req.SearchType = CompletedRemittanceListSearchCode.Nothing;
            Req.SearchTypeSpecified = true;
            Req.PageIndex = PageIndex;
            Req.PageIndexSpecified = true;

            return Req;
        }

        private GetSettlementDetailRequestT Generate_SyncSettlementDetail(int OrderNo)
        {
            GetSettlementDetailRequestT Req = new GetSettlementDetailRequestT();
            Req.orderNo = OrderNo;
            Req.orderNoSpecified = true;

            return Req;
        }

        /// <summary>
        /// datarow는 core에서 지원안됨
        /// </summary>
        /// <param name="Dr"></param>
        /// <returns></returns>
        private async Task<DoShippingGeneralRequestT> Generate_SetDeliveryOrderInfoAsync()/*DataRow Dr*/
        {
            DoShippingGeneralRequestT generalReq = new DoShippingGeneralRequestT();
            ShippingMethodT shippingMtd = new ShippingMethodT();
            RemittanceMethodT remittance = new RemittanceMethodT();
            GetMyAccountRequestT req = new GetMyAccountRequestT();
            GetMyAccountResponseT accRes = new GetMyAccountResponseT();
            accRes = await AuctionClient.GetMyAccountAsync(req);

            remittance.RemittanceMethodType = RemittanceMethodCode.Emoney;
            remittance.RemittanceMethodTypeSpecified = true;
            remittance.RemittanceAccountName = accRes.MyAccount.AccountName;
            remittance.RemittanceAccountNumber = accRes.MyAccount.AccountNumber;
            remittance.RemittanceBankCode = accRes.MyAccount.BankCode;

            string deliDate = string.Empty;// Dr["Deli_Date"].ToString();
            string deliveryNo = string.Empty;// Dr["DeliveryNo"].ToString();
            string bpOffNm = string.Empty;// Dr["BpOffNm"].ToString();
            string pOrdDId = string.Empty;// Dr["POrdD_ID"].ToString();

            if (bpOffNm.Contains("옐로우") || bpOffNm.ToLower().Contains("kg로지스"))
            {
                bpOffNm = "동부익스프레스택배";
            }

            // 발송정보
            DateTime shippingDate = DateTime.Now;
            DateTime.TryParse(deliDate, out shippingDate);
            shippingMtd.SendDate = shippingDate;
            deliveryNo = Regex.Replace(deliveryNo, "[^0-9]", "", RegexOptions.IgnoreCase); //숫자외에 문자는 삭제처리함
            shippingMtd.InvoiceNo = deliveryNo;
            shippingMtd.MessageForBuyer = "";
            shippingMtd.ShippingMethodClassficationType = ShippingMethodClassficationCode.Door2Door;
            shippingMtd.DeliveryAgency = Get_DeliveryAgency(bpOffNm);
            //shippingMtd.DeliveryAgencyName = bpOffNm = bpOffNm.Contains("옐로우") ? "동부익스프레스택배" : bpOffNm; //옥션 요청 옐로우택배, KG로지스 인경우 동부택배로 20151127_이호준
            shippingMtd.DeliveryAgencyName = bpOffNm; //옥션 요청 옐로우택배, KG로지스 인경우 동부택배로 20151127_이호준
            shippingMtd.SendDateSpecified = true;
            shippingMtd.ShippingMethodClassficationTypeSpecified = true;
            shippingMtd.DeliveryAgencySpecified = true;
            shippingMtd.ShippingEtcMethodSpecified = false;

            // 배송정보
            generalReq.OrderNo = Convert.ToInt32(pOrdDId);
            generalReq.RemittanceMethod = remittance;
            generalReq.ShippingMethod = shippingMtd;
            generalReq.OrderNoSpecified = true;

            return generalReq;
        }
        #endregion


        #region 기타 메소드
        // 출고 택배사 
        public DeliveryAgencyWithNothingCode Get_DeliveryAgency(string deliveryAgencyName)
        {
            DeliveryAgencyWithNothingCode DeliveryAgency;
            deliveryAgencyName = deliveryAgencyName.Replace(" ", "");

            if (deliveryAgencyName.Contains("CJGLS"))
                DeliveryAgency = DeliveryAgencyWithNothingCode.cjgls;
            else if (deliveryAgencyName.Contains("한진"))
                DeliveryAgency = DeliveryAgencyWithNothingCode.hanjin;
            else if (deliveryAgencyName.Contains("우체국"))
                DeliveryAgency = DeliveryAgencyWithNothingCode.epost;
            else if (deliveryAgencyName.Contains("현대"))
                DeliveryAgency = DeliveryAgencyWithNothingCode.hyundai;
            else if (deliveryAgencyName.Contains("롯데")) //20170120 현태택배 -> 롯데택배로 변경 옥션값은 그대로 현대
                DeliveryAgency = DeliveryAgencyWithNothingCode.hyundai;
            else if (deliveryAgencyName.Contains("대한통운"))
                DeliveryAgency = DeliveryAgencyWithNothingCode.korex;
            else if (deliveryAgencyName.Contains("로젠"))
                DeliveryAgency = DeliveryAgencyWithNothingCode.kgb;
            //옥션 요청 옐로우택배, KG로지스 인경우 동부택배로 20151127_이호준
            else if (deliveryAgencyName.Contains("옐로우"))
                DeliveryAgency = DeliveryAgencyWithNothingCode.dongbu;
            else if (deliveryAgencyName.ToLower().Contains("kg로지스"))
                DeliveryAgency = DeliveryAgencyWithNothingCode.dongbu;
            else if (deliveryAgencyName.Contains("KGB특급"))
                DeliveryAgency = DeliveryAgencyWithNothingCode.kgbls;
            //else if (deliveryAgencyName.Contains("KT로지스"))
            //    DeliveryAgency = DeliveryAgencyWithNothingCode.ktlogistics;
            //else if (deliveryAgencyName.Contains("세덱스"))
            //    DeliveryAgency = DeliveryAgencyWithNothingCode.sedex;
            else if (deliveryAgencyName.Contains("사가와"))
                DeliveryAgency = DeliveryAgencyWithNothingCode.sagawa;
            //else if (deliveryAgencyName.Contains("하나로"))
            //    DeliveryAgency = DeliveryAgencyWithNothingCode.hanaro;
            else if (deliveryAgencyName.Contains("동부"))
                DeliveryAgency = DeliveryAgencyWithNothingCode.dongbu;
            //else if (deliveryAgencyName.Contains("로엑스"))
            //    DeliveryAgency = DeliveryAgencyWithNothingCode.ajutb;
            //else if (deliveryAgencyName.Contains("아주"))
            //    DeliveryAgency = DeliveryAgencyWithNothingCode.ajutb;
            //else if (deliveryAgencyName.Contains("네덱스"))
            //    DeliveryAgency = DeliveryAgencyWithNothingCode.nedex;
            //else if (deliveryAgencyName.Contains("이노지스"))
            //    DeliveryAgency = DeliveryAgencyWithNothingCode.innogis;
            else
                DeliveryAgency = DeliveryAgencyWithNothingCode.etc;

            return DeliveryAgency;
        }
        #endregion
    }
}
