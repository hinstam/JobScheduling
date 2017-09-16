using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace JobScheduling.Entity.CommModel
{
    public class UploadFile
    {
        [Required(ErrorMessage = " * Required")]
        public int uid { get; set; }

        [Required(ErrorMessage=" * Required")]
        public string UploadBy { get; set; }

        [Required(ErrorMessage = " * Required")]
        public DateTime UploadTime { get; set; }

        [Required(ErrorMessage = " * Required")]
        public string DocName { get; set; }

        [Required(ErrorMessage = " * Required")]
        public string DocActualName { get; set; }

        [Required(ErrorMessage = " * Required")]
        public string DocPath { get; set; }

        [Required(ErrorMessage = " * Required")]
        public int ProjectInfoUID { get; set; }

        public string DocType { get; set; }

        [Required(ErrorMessage = " * Required")]
        public string FileType { get; set; }
    }
}
