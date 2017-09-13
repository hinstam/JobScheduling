using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace JobScheduling.Model.MasterModel
{
    public class CountryM
    {
        [Required(ErrorMessage = " * Required")]
        [StringLength(2, ErrorMessage = " * Must be less than 2 characters")]
        public string Code { get; set; }

        [Required(ErrorMessage = " * Required")]
        [StringLength(32, ErrorMessage = " * Must be less than 32 characters")]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public string Filename { get; set; }
        public string ExportFilename { get; set; }
        public string Errmsg { get; set; }
        public bool HasResult { get; set; }
        public long NumOfSuccess { get; set; }
        public long NumOfData { get; set; }
        public long NumOfFail { get; set; }
        public long NumOfUpdateTrans { get; set; }
        
    }
}
