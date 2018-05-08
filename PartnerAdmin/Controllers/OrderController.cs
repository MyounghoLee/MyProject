using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PartnerAdmin.Controllers
{
    public class OrderController : BaseController
    {
        public ActionResult OrderList()
        {
            return View();
        }

        public ActionResult OrderCancel()
        {
            return View();
        }

        public ActionResult OrderReturn()
        {
            return View();
        }
    }
}