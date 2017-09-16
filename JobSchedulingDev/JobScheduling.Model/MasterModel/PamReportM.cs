using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Model.MasterModel
{
    public class PamReportM:RegonViewModel
    {
        public string fm_date { get; set; }
        public string to_date { get; set; }
        //public string fm_region { get; set; }
        //public string to_region { get; set; } 
        public bool HongKong { get; set; }
        public bool Macau { get; set; }
        public bool China { get; set; }
        public bool Singapore { get; set; }

        public string RegionCodeList { get; set; }


    }
}
