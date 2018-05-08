using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PartnerAdmin.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class BaseController : Controller
    {
        public BaseController()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(User.Identity.IsAuthenticated == false)
            {
                Response.Redirect("/Member/Login");
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}