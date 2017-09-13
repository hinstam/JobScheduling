using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Model.MasterModel
{
    public class UpdateTransM
    {
        public bool HasResult { get; set; }
        public long NumOfSuccess { get; set; }
        public long NumOfData { get; set; }
        public long NumOfFail { get; set; }
        public long NumOfUpdateTrans { get; set; }
    }
}
