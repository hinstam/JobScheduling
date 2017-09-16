using JobScheduling.Business.ReportBL;
using JobScheduling.Model.ReportModel;
using JobScheduling.Web.Models;
using JobScheduling.Web.Extensions;
using JobScheduling.Web.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobScheduling.Business.FileBL;
using JobScheduling.Business.CommonBL;
using JobScheduling.Business.MasterBL;
using JobScheduling.Model.MasterModel;
using JobScheduling.Model.FileModel;


namespace JobScheduling.Web.Controllers
{
    public class SpecificCountryController : CCASController
    {
        //
        // GET: /SpecificCountry/

        public ActionResult Index()
        {
            StoreFileBL storeFileBL = new StoreFileBL();
            CountryBL countryBL = new CountryBL();
            RegionBL regionBL = new RegionBL();
            //binding data to viewdata as selectlist
            //ViewData["countrylist"] = new SelectList(storeFileBL.GetCountryList(), "CountryCode", "CountryDescription");
            ViewData["countrylist"] = new SelectList(countryBL.GetAllCountry(string.Empty), "Code", "Description");
            ViewData["districtlist"] = new SelectList(storeFileBL.GetDistrictList(string.Empty), "DistrictID", "DistrictDescription");
            ViewData["descriptionlist"] = new SelectList(storeFileBL.GetStoreDescriptionList(string.Empty), "StoreCode", "StoreLastDescription");
            //ViewData["regionlist"]=new SelectList(regionBL.GetRegionList(),"CountryID","CountryDescription");

            SpecificCountrySearchM specificCountrySearchM = new SpecificCountrySearchM();
            List<RegionM> selectedRegionList = new List<RegionM>();
            specificCountrySearchM.AvailableRegions = regionBL.GetRegionList(string.Empty);
            specificCountrySearchM.SelectedRegions = selectedRegionList;
            return View(specificCountrySearchM);
        }


        public ActionResult PrintReport(SpecificCountrySearchM model, string type)
        {
            if (model == null)
                model = new SpecificCountrySearchM();

            SpecificCountryBL specificCountryBL = new SpecificCountryBL();
            string ls_fmdate = null, ls_todate = null;
            if (model.FromDate != null && model.FromDate != "")
                ls_fmdate = model.FromDate.Substring(0, 4) + "/" + model.FromDate.Substring(4, 2) + "/" + model.FromDate.Substring(6, 2);

            if (model.ToDate != null && model.ToDate != "")
                ls_todate = model.ToDate.Substring(0, 4) + "/" + model.ToDate.Substring(4, 2) + "/" + model.ToDate.Substring(6, 2);

           

            List<ReportParameter> ps = new List<ReportParameter>();

            if (!string.IsNullOrEmpty(model.CountryCode))
            {
                CountryBL countryBL = new CountryBL();

                CountryM country = countryBL.GetCountry(model.CountryCode);

                ps.Add(new ReportParameter("Country", country.Description));
            }

            if (!string.IsNullOrEmpty(model.StoreCode))
            {
                StoreFileBL storeBL = new StoreFileBL();
                StoreFileM store = storeBL.GetStoreFileByCode(model.StoreCode);
                ps.Add(new ReportParameter("Store", store.StoreLastDescription));
                ps.Add(new ReportParameter("District", store.DistrictDescription));
                ps.Add(new ReportParameter("Region", store.CountryDescription));
            }
            else
            {
                if (!string.IsNullOrEmpty(model.DistrictID))
                {
                    DistrictBL districtBL = new DistrictBL();
                    DistrictM districtM = districtBL.GetDistrictByID(model.DistrictID);
                    ps.Add(new ReportParameter("District", districtM.DistrictDescription));
                    ps.Add(new ReportParameter("Region", districtM.RegionDescription));
                }
                else {
                    RegionBL regionBl = new RegionBL();
                    string regionIDs = string.Empty;
                    string regionDescription = string.Empty;
                    string regionCode = string.Empty;

                    IList<RegionM> regionList = new List<RegionM>();
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
                        //regionCode = regionCode.Substring(0, regionCode.Length - 1);
                        //model.RegionCodeList = regionCode;
                        //ps.Add(new ReportParameter("Region", regionDescription));

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
                    regionCode = regionCode.Substring(0, regionCode.Length - 1);
                    model.RegionCodeList = regionCode;
                    ps.Add(new ReportParameter("Region", regionDescription));
                }
            }

            if (ls_fmdate == null || ls_fmdate == "")
            {
                if (specificCountryBL.GetFirstLastDate().Rows[0]["min"].ToString() != null && specificCountryBL.GetFirstLastDate().Rows[0]["min"].ToString() != "")
                    ls_fmdate = DateTime.Parse(specificCountryBL.GetFirstLastDate().Rows[0]["min"].ToString()).ToString("yyyy/MM/dd");
            }
            ps.Add(new ReportParameter("FromDate", ls_fmdate));
            if (ls_todate == null || ls_todate == "")
            {
                if (specificCountryBL.GetFirstLastDate().Rows[0]["max"].ToString() != null && specificCountryBL.GetFirstLastDate().Rows[0]["max"].ToString() != "")
                    ls_todate = DateTime.Parse(specificCountryBL.GetFirstLastDate().Rows[0]["max"].ToString()).ToString("yyyy/MM/dd");
            }
            ps.Add(new ReportParameter("ToDate", ls_todate));


            var source = specificCountryBL.GetSpecificCountrySum(model);

            SpecificCountryM m = specificCountryBL.GetSum(model);


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

            //ps.Add(new ReportParameter("Region", region));
            //ps.Add(new ReportParameter("FromRegion", model.FromRegion));
            //ps.Add(new ReportParameter("ToRegion", model.ToRegion));
            ps.Add(new ReportParameter("TotalTransaction", m.TotalTransaction.ToString()));
            ps.Add(new ReportParameter("TotalAmount", m.TotalAmount.ToString()));

            //ReportM result = new ReportM();
            //result.ReportPath = "/Content/Report/SpecificCountry.rdlc";
            //result.Parameters = ps;
            //result.DataSource = new ReportDataSource("DataSet1", source);
            //return View("../Shared/_PartialReport", result);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.LocalReport.ReportPath = Server.MapPath("/Content/Report/SpecificCountry.rdlc");
            reportViewer.LocalReport.SetParameters(ps);
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", source));
            reportViewer.LocalReport.Refresh();

            if (string.IsNullOrEmpty(type))
            {
                //LocalReport localReport = new LocalReport();
                //localReport.ReportPath = Server.MapPath("/Content/Report/SpecificCountry.rdlc");
                //localReport.SetParameters(ps);
                //localReport.DataSources.Add(new ReportDataSource("DataSet1", source));

                return File(PDFSharedBL.LocalReportToByteV(reportViewer.LocalReport), "application/pdf");
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

                return File(bytes, mimeType, "Summary Report – Specific Country(Of Issuing Bank)." + extension);
            }
        }

        [HttpPost]
        public ActionResult CheckData(SpecificCountrySearchM model)
        {
            bool result = true;
            string ls_time = "";
            if (model.FromDate != null && model.FromDate != "")
                try
                {
                    ls_time = model.FromDate.Substring(0, 4) + "/" + model.FromDate.Substring(4, 2) + "/" + model.FromDate.Substring(6, 2);
                    DateTime datetime = DateTime.Parse(ls_time);
                    result = true;
                }
                catch
                {
                    result = false;
                    return Json(result);
                }
            if (model.ToDate != null && model.ToDate != "")
                try
                {
                    ls_time = model.ToDate.Substring(0, 4) + "/" + model.ToDate.Substring(4, 2) + "/" + model.ToDate.Substring(6, 2);
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

        [HttpPost]
        public JsonResult GetRegionRelateInfo(string regionIDs)
        {
            RegionRelateViewModel regionViewModel = new RegionRelateViewModel();

            StoreFileBL storeFileBL=new StoreFileBL();
            DistrictBL districtBL=new DistrictBL();
            try
            {
                regionViewModel.StoreList = storeFileBL.GetStoreDescriptionListByRegionIDs(regionIDs);
                regionViewModel.DistrictList = districtBL.GetDistrictList(regionIDs);

            }
            catch (Exception ex)
            {

            }
            return Json(regionViewModel);
        }
    }
}
