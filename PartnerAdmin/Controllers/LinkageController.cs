using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PartnerAdmin.Controllers
{
    public class LinkageController : Controller
    {
        public IActionResult LinkageErrorList()
        {
            return View();
        }

        public IActionResult LinkageRatePartner()
        {
            return View();
        }

        public IActionResult MappingRegister()
        {
            return View();
        }

        public IActionResult MappingRegisterError()
        {
            return View();
        }
    }
}