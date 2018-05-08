using System;
using System.Collections.Generic;
using System.Text;
using OfficeOpenXml;
using System.Data;
using System.Collections;

namespace biz.CommonBiz
{
    public class CommonExcelPackage
    {
        ///DataTable은 core2.0부터 지원함(현재 2017-06-07 core2.0 preview버전)_ 이명호
        ///// <summary>
        ///// 엑셀 출력
        ///// </summary>
        ///// <param name="dsResult">엑셀 출력 내용 DataTable</param>
        ///// <param name="columns">DataGrid 컬럼 리스트</param>
        //public void SaveAsExcel(DataTable dsResult, ArrayList columns)
        //{
        //    SaveAsExcel(dsResult, columns, "", "");
        //}

        ///// <summary>
        ///// 엑셀 출력
        ///// </summary>
        ///// <param name="dsResult">엑셀 출력 내용 DataTable</param>
        ///// <param name="columns">DataGrid 컬럼 리스트</param>
        ///// <param name="subject">DataGrid Caption 명</param>
        ///// <param name="fileName">생성 엑셀 파일명</param>
        //public void SaveAsExcel(DataTable dsResult, ArrayList columns, string subject, string fileName)
        //{
        //    // 다운로드시 사용할 파일명을 설정
        //    if (string.IsNullOrEmpty(fileName))
        //    {
        //        fileName = DateTime.Now.ToString("yyyyMMdd") + ".xls";
        //    }

        //    System.Web.HttpContext.Current.Response.Buffer = true;

        //    // DataGrid를 생성
        //    DataGrid dgExcel = new DataGrid();
        //    dgExcel.ShowHeader = true;

        //    if (string.IsNullOrEmpty(subject))
        //    {
        //        dgExcel.Caption = subject;
        //    }

        //    // DataGrid에 Header 텍스트를 추가
        //    dgExcel.AutoGenerateColumns = false;

        //    foreach (object column in columns)
        //    {
        //        dgExcel.Columns.Add((BoundColumn)column);
        //    }

        //    // DataGrid의 스타일을 지정

        //    dgExcel.HeaderStyle.BackColor = Color.FromName("powderblue");
        //    dgExcel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        //    dgExcel.HeaderStyle.Height = 25;
        //    dgExcel.HeaderStyle.Font.Bold = true;

        //    // DataGrid에 DataSet의 내용을 채움
        //    dgExcel.DataSource = dsResult;
        //    dgExcel.DataBind();

        //    string style = @"<style> td { mso-number-format:\@; } </style> ";

        //    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName));
        //    //System.Web.HttpContext.Current.Response.ContentType = "application/unknown";
        //    System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        //    System.Web.HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content='text/html; charset=euc-kr'>");
        //    System.Web.HttpContext.Current.Response.Write(style);

        //    dgExcel.EnableViewState = false;

        //    System.IO.StringWriter sWriter = new System.IO.StringWriter();
        //    System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(sWriter);

        //    dgExcel.RenderControl(htmlWriter);

        //    System.Web.HttpContext.Current.Response.Write(sWriter.ToString());
        //    System.Web.HttpContext.Current.Response.End();

        //    dgExcel.Dispose();
        //}

        ///// <summary>
        ///// BoundColumn 생성
        ///// </summary>
        ///// <param name="DataFieldValue">DataField 값</param>
        ///// <param name="HeaderTextValue">HeaderText 값</param>
        ///// <returns></returns>
        //public BoundColumn CreateBoundColumn(string DataFieldValue, string HeaderTextValue)
        //{
        //    BoundColumn column = new BoundColumn();

        //    column.DataField = DataFieldValue;
        //    column.HeaderText = HeaderTextValue;

        //    return column;
        //}

        ///// <summary>
        ///// BoundColumn 생성
        ///// </summary>
        ///// <param name="DataFieldValue">DataField 값</param>
        ///// <param name="HeaderTextValue">HeaderText 값</param>
        ///// <param name="FormatValue">DataFormat 값</param>
        ///// <param name="AlignValue">정렬값</param>
        ///// <returns></returns>
        //public BoundColumn CreateBoundColumn(string DataFieldValue, string HeaderTextValue, string FormatValue, HorizontalAlign AlignValue)
        //{
        //    BoundColumn column = CreateBoundColumn(DataFieldValue, HeaderTextValue);

        //    column.DataFormatString = FormatValue;
        //    column.ItemStyle.HorizontalAlign = AlignValue;

        //    return column;
        //}

        ///// <summary>
        ///// 엑셀 출력
        ///// </summary>
        ///// <param name="dsResult"></param>
        ///// <param name="fileName"></param>
        //public void SaveAsExcel(DataTable dsResult, string fileName)
        //{
        //    DataGrid dgList = new DataGrid();

        //    dgList.DataSource = dsResult;
        //    dgList.DataBind();

        //    if (string.IsNullOrEmpty(fileName))
        //    {
        //        fileName = DateTime.Now.ToString("yyyyMMdd") + ".xls";
        //    }

        //    string style = @"<style> td { mso-number-format:\@; } </style> ";

        //    System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        //    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;fileName=" + fileName);
        //    System.Web.HttpContext.Current.Response.Charset = "";

        //    dgList.EnableViewState = false;

        //    System.IO.StringWriter tw = new System.IO.StringWriter();
        //    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        //    dgList.RenderControl(hw);

        //    string convDgList = tw.ToString();
        //    convDgList = convDgList.Replace("&nbsp;", " ");

        //    string convStr = @"<html><head><title>주문내역</title><meta http-equiv=""Content-Type"" content=""text/html; charset=euc-kr"">";
        //    convStr += style;
        //    convStr += @"<Body>" + convDgList;
        //    convStr += @"</Body></html>";

        //    System.Web.HttpContext.Current.Response.Write(convStr);
        //    System.Web.HttpContext.Current.Response.End();
        //}

        ///// <summary>
        ///// 엑셀파일다운 단일 시트
        ///// </summary>
        ///// <param name="page"></param>
        ///// <param name="fInfo"></param>
        ///// <param name="contentsList"></param>
        //public void SaveAsExcel(Page page, FileInfo fInfo, DataTable dataTable)
        //{
        //    using (ExcelPackage excelPackage = new ExcelPackage(fInfo))
        //    {

        //        var sheet = excelPackage.Workbook.Worksheets.Add("sheet1");
        //        sheet.Cells.LoadFromDataTable(dataTable, true);
        //        using (var range = sheet.Cells[1, 1, 1, dataTable.Columns.Count])   //헤더 스타일 설정
        //        {
        //            range.Style.Font.Bold = true;
        //            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(183, 240, 177));
        //            range.AutoFitColumns();
        //        }

        //        for (int row = 0; row <= dataTable.Rows.Count; row++)   //테두리 설정(BrandMap)
        //        {
        //            for (int col = 1; col <= dataTable.Columns.Count; col++)
        //            {
        //                using (var range = sheet.Cells[row + 1, col, row + 1, col])
        //                {
        //                    if (range.Value != null)
        //                    {
        //                        range.Value = range.Value.ToString();
        //                    }

        //                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                }
        //            }

        //        }
        //        excelPackage.Save();

        //        page.Response.Clear();
        //        page.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        page.Response.AddHeader("Content-Disposition", "attachment;fileName=" + fInfo.Name);
        //        page.Response.Charset = "euc-kr";
        //        page.Response.ContentEncoding = Encoding.GetEncoding("euc-kr");
        //        page.Response.TransmitFile(fInfo.FullName);
        //        page.Response.Flush();
        //        fInfo.Delete(); //파일다운완료시 삭제
        //        page.Response.End();
        //    }
        //}


        ///// <summary>
        ///// 엑셀파일다운 다중 시트
        ///// </summary>
        ///// <param name="page"></param>
        ///// <param name="fInfo"></param>
        ///// <param name="contentsList"></param>
        //public void SaveAsExcel(Page page, FileInfo fInfo, List<DataTable> listDataTable)
        //{
        //    int index = 0;

        //    using (ExcelPackage excelPackage = new ExcelPackage(fInfo))
        //    {
        //        foreach (DataTable dt in listDataTable)
        //        {
        //            index++;
        //            var sheet = excelPackage.Workbook.Worksheets.Add(string.Format("sheet{0}", index));
        //            sheet.Cells.LoadFromDataTable(dt, true);
        //            using (var range = sheet.Cells[1, 1, 1, dt.Columns.Count])   //헤더 스타일 설정
        //            {
        //                range.Style.Font.Bold = true;
        //                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(183, 240, 177));
        //                range.AutoFitColumns();
        //            }

        //            for (int row = 0; row <= dt.Rows.Count; row++)   //테두리 설정(BrandMap)
        //            {
        //                for (int col = 1; col <= dt.Columns.Count; col++)
        //                {
        //                    using (var range = sheet.Cells[row + 1, col, row + 1, col])
        //                    {
        //                        if (range.Value != null)
        //                        {
        //                            range.Value = range.Value.ToString();
        //                        }

        //                        range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                    }
        //                }
        //            }
        //        }

        //        excelPackage.Save();

        //        page.Response.Clear();
        //        page.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        page.Response.AddHeader("Content-Disposition", "attachment;fileName=" + fInfo.Name);
        //        page.Response.Charset = "euc-kr";
        //        page.Response.ContentEncoding = Encoding.GetEncoding("euc-kr");
        //        page.Response.TransmitFile(fInfo.FullName);
        //        page.Response.Flush();
        //        fInfo.Delete(); //파일다운완료시 삭제
        //        page.Response.End();
        //    }
        //}

        ///// <summary>
        ///// 엑셀파일다운(매핑리스트 다운 전용)
        ///// </summary>
        ///// <param name="page"></param>
        ///// <param name="fInfo"></param>
        ///// <param name="contentsList"></param>
        //public void SaveAsExcel(FileInfo fInfo, DataTable dtBrandMapList, DataTable dtItemMapList)
        //{
        //    using (ExcelPackage excelPackage = new ExcelPackage(fInfo))
        //    {

        //        var brandMapSheet = excelPackage.Workbook.Worksheets.Add(string.Format("brandMap"));
        //        brandMapSheet.Cells.LoadFromDataTable(dtBrandMapList, true);
        //        using (var range = brandMapSheet.Cells[1, 1, 1, dtBrandMapList.Columns.Count])   //헤더 스타일 설정(BrandMap)
        //        {
        //            range.Style.Font.Bold = true;
        //            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(183, 240, 177));
        //            range.AutoFitColumns();
        //        }

        //        for (int row = 0; row <= dtBrandMapList.Rows.Count; row++)   //테두리 설정(BrandMap)
        //        {
        //            for (int col = 1; col <= dtBrandMapList.Columns.Count; col++)
        //            {
        //                using (var range = brandMapSheet.Cells[row + 1, col, row + 1, col])
        //                {
        //                    if (range.Value != null)
        //                    {
        //                        range.Value = range.Value.ToString();
        //                    }

        //                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                }
        //            }
        //        }

        //        var itemMapSheet = excelPackage.Workbook.Worksheets.Add(string.Format("itemMap"));
        //        itemMapSheet.Cells.LoadFromDataTable(dtItemMapList, true);
        //        using (var range = itemMapSheet.Cells[1, 1, 1, dtItemMapList.Columns.Count])   //헤더 스타일 설정(BrandMap)
        //        {
        //            range.Style.Font.Bold = true;
        //            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(183, 240, 177));
        //            range.AutoFitColumns();
        //        }

        //        for (int row = 0; row <= dtItemMapList.Rows.Count; row++)   //테두리 설정(BrandMap)
        //        {
        //            for (int col = 1; col <= dtItemMapList.Columns.Count; col++)
        //            {
        //                using (var range = itemMapSheet.Cells[row + 1, col, row + 1, col])
        //                {
        //                    if (range.Value != null)
        //                    {
        //                        range.Value = range.Value.ToString();
        //                    }

        //                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                }
        //            }
        //        }

        //        excelPackage.Save();
        //    }
        //}

        ///// <summary>
        ///// 엑셀을 데이터 테이블에 입력
        ///// </summary>
        ///// <param name="page"></param>
        ///// <param name="fInfo"></param>
        ///// <param name="contentsList"></param>
        //public List<DataTable> GetDataTableAsExcel(FileUpload upload, int sheetTotalIndex)
        //{
        //    List<DataTable> resultList = new List<DataTable>();
        //    DataTable dt = new DataTable();

        //    using (ExcelPackage excelPackage = new ExcelPackage(upload.FileContent))
        //    {
        //        for (int sheetIndex = 1; sheetIndex <= sheetTotalIndex; sheetIndex++)
        //        {
        //            dt = ToDataTable(excelPackage, sheetIndex);
        //            resultList.Add(dt);
        //        }
        //    }

        //    return resultList;
        //}

        ///// <summary>
        ///// 엑셀 데이터 테이블 Convert
        ///// </summary>
        ///// <param name="package"></param>
        ///// <param name="sheetTotalIndex"></param>
        ///// <returns></returns>
        //private DataTable ToDataTable(ExcelPackage package, int sheetTotalIndex)
        //{
        //    using (ExcelWorksheet workSheet = package.Workbook.Worksheets[sheetTotalIndex])
        //    {
        //        DataTable table = new DataTable();
        //        foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
        //        {
        //            table.Columns.Add(firstRowCell.Text);
        //        }

        //        for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
        //        {
        //            bool isRowCheck = false;
        //            var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
        //            var newRow = table.NewRow();
        //            foreach (var cell in row)
        //            {
        //                newRow[cell.Start.Column - 1] = cell.Text;
        //                if (!string.IsNullOrEmpty(cell.Text))
        //                {
        //                    isRowCheck = true;
        //                }
        //            }

        //            if (isRowCheck)
        //            {
        //                table.Rows.Add(newRow);
        //            }
        //        }
        //        return table;
        //    }
        //}
    }
}
