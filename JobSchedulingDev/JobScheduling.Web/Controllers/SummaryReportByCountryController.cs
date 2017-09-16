using JobScheduling.Business.MasterBL;
using JobScheduling.Model.CommModel;
using JobScheduling.Model.MasterModel;
using JobScheduling.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using JobScheduling.Web.Models;

namespace JobScheduling.Web.Controllers
{
    public class SummaryReportByCountryController : CCASController
    {
        //
        // GET: /SummaryReportByCountry/

        public ActionResult Index()
        {
            RegionBL regionBL = new RegionBL();
            PamReportM pamReportM = new PamReportM();
            List<RegionM> selectedRegionList = new List<RegionM>();
            pamReportM.AvailableRegions = regionBL.GetRegionList(string.Empty);
            pamReportM.SelectedRegions = selectedRegionList;
            return View(pamReportM);
        }


        public ActionResult PrintReport(PamReportM model, string type)
        {
            SummaryReportByCountryBL sbcbl = new SummaryReportByCountryBL();
            string ls_fmdate = null, ls_todate = null;
            if (model.fm_date != null && model.fm_date != "")
                ls_fmdate = model.fm_date.Substring(0, 4) + "/" + model.fm_date.Substring(4, 2) + "/" + model.fm_date.Substring(6, 2);

            if (model.to_date != null && model.to_date != "")
                ls_todate = model.to_date.Substring(0, 4) + "/" + model.to_date.Substring(4, 2) + "/" + model.to_date.Substring(6, 2);

            model.fm_date = ls_fmdate;
            model.to_date = ls_todate;
           

            List<ReportParameter> myParameterList = new List<ReportParameter>();
            //    ps.Add(new ReportParameter("PrintBy", SettlementReportBL.UserName == null ? "James" : SettlementReportBL.UserName));
            SummaryReportByCountryM result = new SummaryReportByCountryM();

            if (model.fm_date == null || model.fm_date == "")
            {
                if (sbcbl.GetFirstLastDate().Rows[0]["min"].ToString() != null && sbcbl.GetFirstLastDate().Rows[0]["min"].ToString() != "")
                    model.fm_date = DateTime.Parse(sbcbl.GetFirstLastDate().Rows[0]["min"].ToString()).ToString("yyyy/MM/dd");
            }
            myParameterList.Add(new ReportParameter("Fm_date", model.fm_date));

            if (model.to_date == null || model.to_date == "")
            {
                if (sbcbl.GetFirstLastDate().Rows[0]["min"].ToString() != null && sbcbl.GetFirstLastDate().Rows[0]["max"].ToString() != "")
                    model.to_date = DateTime.Parse(sbcbl.GetFirstLastDate().Rows[0]["max"].ToString()).ToString("yyyy/MM/dd");
            }

            myParameterList.Add(new ReportParameter("To_date", model.to_date));



            //myParameterList.Add(new ReportParameter("Fm_region", Model.fm_region));
            //myParameterList.Add(new ReportParameter("To_region", Model.to_region));
            //string region = "";
            //if (model.HongKong)
            //    region += ",Hong Kong";
            //if (model.Macau)
            //    region += ",Macau";
            //if (model.China)
            //    region += ",China";
            //if (model.Singapore)
            //    region += ",Singapore";
            //if (region != "")
            //    region = region.Substring(1);

            string regionIDs = string.Empty;
            string regionDescription = string.Empty;
            string regionCode = string.Empty;
            RegionBL regionBl = new RegionBL();
            IList<RegionM> regionList=new List<RegionM>();

            if (model.Postedregions != null)
            {
                for (int i = 0; i < model.Postedregions.RegionIDs.Length; i++)
                {
                    regionIDs += model.Postedregions.RegionIDs[i] + ",";
                }
                regionIDs = regionIDs.Substring(0, regionIDs.Length - 1);
                regionList = regionBl.GetRegionList(regionIDs);
                //foreach (var item in regionList)
                //{
                //    regionDescription += item.RegionDescription + ",";
                //    regionCode += item.RegionCode + ",";
                //}

                //regionDescription = regionDescription.Substring(0, regionDescription.Length - 1);
                //model.RegionCodeList = regionCode.Substring(0,regionCode.Length-1);
                //myParameterList.Add(new ReportParameter("Region", regionDescription));

            }
            else
            {
                regionList = regionBl.GetRegionList(string.Empty);
            }

            foreach (var item in regionList)
            {
                regionDescription += item.RegionDescription + ",";
                regionCode += item.RegionCode + ",";
            }

            regionDescription = regionDescription.Substring(0, regionDescription.Length - 1);
            model.RegionCodeList = regionCode.Substring(0, regionCode.Length - 1);
            myParameterList.Add(new ReportParameter("Region", regionDescription));
            //myParameterList.Add(new ReportParameter("Region", region));


            var source = sbcbl.GetComparison(model);
            SummaryReportByCountryM m = sbcbl.GetTotalSum(model);
            myParameterList.Add(new ReportParameter("TotalTransaction", m.TotalTransaction.ToString()));
            myParameterList.Add(new ReportParameter("TotalAmount", m.TotalAmount.ToString()));


            //LocalReport localReport = new LocalReport();
            //localReport.ReportPath = Server.MapPath("/Content/Report/SummaryReport_ByCountry.rdlc");
            //localReport.SetParameters(myParameterList);
            //localReport.DataSources.Add(new ReportDataSource("DataSet1", source));
            //return File(SummaryReportByCountryBL.LocalReportToByteV(localReport), "application/pdf");
            //return View();

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.LocalReport.ReportPath = Server.MapPath("/Content/Report/SummaryReport_ByCountry.rdlc");
            reportViewer.LocalReport.SetParameters(myParameterList);
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", source));
            reportViewer.LocalReport.Refresh();

            if (string.IsNullOrEmpty(type))
            {
                //LocalReport localReport = new LocalReport();
                //localReport.ReportPath = Server.MapPath("/Content/Report/SpecificCountry.rdlc");
                //localReport.SetParameters(ps);
                //localReport.DataSources.Add(new ReportDataSource("DataSet1", source));

                return File(SummaryReportByCountryBL.LocalReportToByteV(reportViewer.LocalReport), "application/pdf");
            }
            else
            {
                Warning[] warnings;
                string mimeType;
                string[] streamids;
                string encoding;
                string extension;

                byte[] bytes = reportViewer.LocalReport.Render(
                    FileType.Excel.ToString(), null, out mimeType, out encoding,
                    out extension, out streamids, out warnings
                );

                //byte[] bytes = ReportConvert.Report2File(reportViewer, FileType.Excel, out mimeType);

                return File(bytes, mimeType, "Summary Report – By Country(Of Issuing Bank)." + extension);
            }

        }

        [HttpPost]
        public ActionResult CheckData(PamReportM model)
        {
            bool result = true;
            string ls_time = "";
            if (model.fm_date != null && model.fm_date != "")
                try
                {
                    ls_time = model.fm_date.Substring(0, 4) + "/" + model.fm_date.Substring(4, 2) + "/" + model.fm_date.Substring(6, 2);
                    DateTime datetime = DateTime.Parse(ls_time);
                    result = true;
                }
                catch
                {
                    result = false;
                    return Json(result);
                }
            if (model.to_date != null && model.to_date != "")
                try
                {
                    ls_time = model.to_date.Substring(0, 4) + "/" + model.to_date.Substring(4, 2) + "/" + model.to_date.Substring(6, 2);
                    DateTime datetime = DateTime.Parse(ls_time);
                    result = true;
                }
                catch
                {
                    result = false;
                    return Json(result);
                }

            return Json(result);
        }

    }
}
