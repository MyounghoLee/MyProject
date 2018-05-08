using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using biz.MainBiz;
using MainBiz.Chart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using models.Product;
using PartnerAdmin.Models.Json;
using PartnerAdmin.Models.Member;
using PartnerAdmin.Models.Order;

namespace PartnerAdmin.Controllers
{
    public class MainController : BaseController
    {
        public IActionResult Main()
        {
            
            return View();
        }

        [HttpPost]
        public JsonResult SearchOrderChart([FromBody]dynamic formData)
        {
            try
            {
                string domCd = formData.domCd;
                int beforeDay = formData.beforeDay;

                OrderChartBiz chartOrderBiz = new OrderChartBiz();
                List<OrderM> targetList = chartOrderBiz.GetOrderChartList<OrderM>(domCd, beforeDay);
                List<OrderM> targetCvtList = new List<OrderM>();
                var partnerIdlist = targetList.GroupBy(g => g.PartnerId).Select(s => s.Key).ToList();

                foreach(string partnerId in partnerIdlist)
                {
                    OrderM om = new OrderM();
                    om.PartnerId = partnerId;
                    om.Values = targetList.Where(w => w.PartnerId == partnerId).Select(s => new
                    {
                        Key = (long)s.Ins_Date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds,
                        Value = s.Cnt
                    }).ToList();

                    targetCvtList.Add(om);
                }

                dynamic list = targetCvtList.AsEnumerable().Select(x => new
                {
                    key = x.PartnerId,
                    values = x.Values
                }).ToList();

                return Json(JsonResultString.GetJsonResultStringConvert(true, list));
            }
            catch (Exception ex)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, "error"));
            }
        }
    }
}