using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EG.CCAS.Model.MasterModel
{
    public class InportTransationM
    {
        public string filename { get; set; }
        public string rundate { get; set; }

        public bool HasResult { get; set; }
        public long NumOfSuccess { get; set; }
        public long NumOfData { get; set; }
        public long NumOfFail { get; set; }
    }
}
