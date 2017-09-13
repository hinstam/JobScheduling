using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobScheduling.Model.MasterModel;
using JobScheduling.Model.FileModel;

namespace JobScheduling.Model.ReportModel
{
    public class SpecificCountryM
    {
        public string BankName { get; set; }
        public string CardBrand { get; set; }
        public decimal? Transactions { get; set; }     
        public decimal? Amount { get; set; }

        public decimal? TotalTransaction { get; set; }
        public decimal? TotalAmount { get; set; }
    }

    public class SpecificCountrySearchM : RegonViewModel
    {
        public string CountryCode { get; set; }
        public string Description { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string District { get; set; }
        public string shop { get; set; }
        //public string[] RegionIDs { get; set; }
        //public string FromRegion { get; set; }
        //public string ToRegion { get; set; }

        public bool HongKong { get; set; }
        public bool Macau { get; set; }
        public bool China { get; set; }
        public bool Singapore { get; set; }

        public string DistrictID { get; set; }
        public string StoreCode { get; set; }
        public string CountryID { get; set; }
        public string RegionCodeList { get; set; }
    }

    public class RegionRelateViewModel
    {
        public IList<DistrictM> DistrictList { get; set; }

        public IList<StoreFileM> StoreList { get; set; }
    }
}
