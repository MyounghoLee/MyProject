using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using biz.MainBiz;
using Microsoft.AspNetCore.Mvc;
using models.Product;
using PartnerAdmin.Models.Json;

namespace PartnerAdmin.Controllers
{
    public class ProductController : BaseController
    {
        public IActionResult ProductAdd()
        {
            return View();
        }

        public IActionResult ProductList()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SearchProduct([FromBody]dynamic formData)
        {
            try
            {
                string partnerId = formData.partnerId;
                string goodId = formData.goodId;
                int currentPageNo = formData.currentPageNo;
                int limitPageNo = formData.limitPageNo;

                BaseMainBiz mainBiz = new BaseMainBiz();
                List<ProductTarget> targetList = mainBiz.GetProductTargetList(partnerId, goodId, currentPageNo, limitPageNo);

                dynamic list = targetList.AsEnumerable().Select(x => new
                {
                    TotalCnt = x.TotalCnt,
                    PartnerID = x.PartnerID,
                    GoodID = x.GoodID,
                    P_GoodID = x.P_GoodID,
                    IsPrice = x.IsPrice,
                    IsOption = x.IsOption,
                    IsState = x.IsState,
                    IsDel = x.IsDel,
                    IsSync = x.IsSync,
                    IsMapping = x.IsMapping,
                    IsLimitQty = x.IsLimitQty,
                    ErrCnt = x.ErrCnt,
                    OrderCnt = x.OrderCnt,
                    RcdDateCvt = x.RcdDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    UptDateCvt = x.UptDate.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList();

                return Json(JsonResultString.GetJsonResultStringConvert(true, list));
            }
            catch(Exception ex)
            {
                return Json(JsonResultString.GetJsonResultStringConvert(false, "error"));
            }
        }

        [HttpPost]
        public FileResult DownloadExcelProduct([FromBody]dynamic formData)
        {
            try
            {
                //ExcelFileClass excelFile = new ExcelFileClass();
                //DataTable dtBrandMapList = new DataTable();
                //DataTable dtItemMapList = new DataTable();
                //string mall = paramMall;
                //string partnerID = paramPartnerId;
                ////string paramBrdList = paramBrdList;
                //string brdList = string.Empty;
                //string url = string.Empty;
                //bool isUseErrorLog = false;

                //string fileName = "MappingList" + "_" + mall + '_' + partnerID + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";
                //dtBrandMapList = GetBrandMappingList(partnerID, mall, paramBrdList, isUseErrorLog);

                //if (dtBrandMapList == null || dtBrandMapList.Rows.Count < 1)
                //{
                //    throw new ApplicationException("데이터가 없습니다.");

                //}
                //else
                //{
                //    foreach (DataRow row in dtBrandMapList.Rows)
                //    {
                //        brdList += row["BrdCd"].ToString().Trim() + ',';
                //    }
                //}

                //dtItemMapList = GetItemMappingList(partnerID, mall, brdList, isUseErrorLog);

                //if (dtBrandMapList == null || dtItemMapList == null)
                //{
                //    throw new ApplicationException("데이터가 없습니다.");
                //}
                //else
                //{
                //    dtBrandMapList = BrandMapList(partnerID, dtBrandMapList);
                //    dtItemMapList = ItemMapList(partnerID, dtItemMapList);
                //}

                //DateTime today = DateTime.Now;
                //string directory = _excelFilesDirectory + "\\" + today.ToString("yyyyMM") + "\\" + today.ToString("yyyyMMdd") + "\\";
                //DirectoryInfo di = new DirectoryInfo(directory);

                //if (!di.Exists)
                //{
                //    di.Create();
                //}

                //FileInfo fInfo = new FileInfo(Path.Combine(directory, fileName));
                //excelFile.SaveAsExcel(fInfo, dtBrandMapList, dtItemMapList);

                //return string.Format("/Mapping/FileDownloaderHandler.ashx?FullFileName=(({0}))&OnlyFileName={1}", fInfo.FullName.Replace("\\", "$$"), fInfo.Name);

                var fileName = $"Excel.exe";
                var filepath = $"Downloads/{fileName}";
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);

                return File(fileBytes, "application/x-msdownload", fileName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}