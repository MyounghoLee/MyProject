﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PartnerAdmin.ViewComponents
{
    public class ChartBoriBoriViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("ChartBoriBori");
        }
    }
}