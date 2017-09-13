﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobScheduling.Model.MasterModel
{
    public class ImportStoreFileM
    {
        //public string StoreCode { get; set; }
        //public string District { get; set; }
        //public string Description { get; set; }

        public string Filename { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Errmsg { get; set; }

        public string ExportFilename { get; set; }
        public bool HasResult { get; set; }
        public long NumOfSuccess { get; set; }
        public long NumOfData { get; set; }
        public long NumOfFail { get; set; }
        public long NumOfSkip { get; set; }
    }
}