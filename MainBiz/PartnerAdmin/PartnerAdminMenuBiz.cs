using System;
using System.Collections.Generic;
using System.Text;
using dac.MainDac.PartnerAdminMenu;

namespace MainBiz.PartnerAdmin
{
    public class PartnerAdminMenuBiz
    {
        private PartnerAdminMenuDac _partnerAdminMenuDac = null;

        public PartnerAdminMenuBiz()
        {
            _partnerAdminMenuDac = new PartnerAdminMenuDac();
        }

        /// <summary>
        /// Nav Admin 메뉴 조회
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetPartnerAdminMenu<T>()
        {
            try
            {
                List<T> adminMenuList = _partnerAdminMenuDac.GetetPartnerAdminMenuList<T>();

                return adminMenuList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
