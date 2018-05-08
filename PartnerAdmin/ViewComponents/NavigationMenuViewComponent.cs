using System.Collections.Generic;
using MainBiz.PartnerAdmin;
using Microsoft.AspNetCore.Mvc;
using PartnerAdmin.Models.PartnerAdminMenu;

namespace PartnerAdmin.ViewComponents
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            PartnerAdminMenuBiz partnerAdminMenu = new PartnerAdminMenuBiz();
            List<PartnerAdminMenu> partnerAdminMenuList = partnerAdminMenu.GetPartnerAdminMenu<PartnerAdminMenu>();

            return View("NavigationMenuDefault", partnerAdminMenuList);
        }
    }
}
