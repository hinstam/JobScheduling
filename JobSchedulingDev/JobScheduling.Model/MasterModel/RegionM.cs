using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Model.MasterModel
{
    public class RegionM
    {
        public Guid RegionID { get; set; }

        public string RegionCode { get; set; }

        public string RegionDescription { get; set; }

        public bool IsSelected { get; set; }

        public object Tags { get; set; }

        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }

    public class RegonViewModel
    {
        public IList<RegionM> AvailableRegions { get; set; }

        public IList<RegionM> SelectedRegions { get; set; }

        public Postedregions Postedregions { get; set; }
    }

    public class Postedregions
    {
        public string[] RegionIDs { get; set; }
    }
}
