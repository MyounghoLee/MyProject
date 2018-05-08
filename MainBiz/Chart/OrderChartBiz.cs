using System;
using System.Collections.Generic;
using System.Text;
using MainDac.Chart;

namespace MainBiz.Chart
{
    public class OrderChartBiz
    {
        private OrderChartDac _chartDac = null;

        public OrderChartBiz()
        {
            _chartDac = new OrderChartDac();
        }

        /// <summary>
        /// siteId로 조회
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public List<T> GetOrderChartList<T>(string domCd, int beforeDay)
        {
            try
            {
                List<T> orderChartList = _chartDac.GetOrderChartList<T>(domCd, beforeDay);

                return orderChartList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
