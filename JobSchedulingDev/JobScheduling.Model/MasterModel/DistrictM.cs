using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Model.MasterModel
{
    public class DistrictM
    {
        public Guid DistrictID { get; set; }

        public string DistrictCode { get; set; }

        public string DistrictDescription { get; set; }

        public Guid RegionID { get; set; }

        public string RegionDescription { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string UpdateBy { get; set; }

    }
}
