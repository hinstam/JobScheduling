using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace JobScheduling.Model.FileModel
{
    public class BinFileM
    {
        public string ManualAuto { get; set; }

        [Required(ErrorMessage = " * Required")]
        [StringLength(6, ErrorMessage = " * Must be less than 6 characters")]
        public string BIN { get; set; }

        [StringLength(20, ErrorMessage = " * Must be less than 20 characters")]
        public string CardBrand { get; set; }

        [StringLength(30, ErrorMessage = " * Must be less than 30 characters")]
        public string IssuingBank { get; set; }

        [StringLength(20, ErrorMessage = " * Must be less than 20 characters")]
        public string TypeofCard { get; set; }

        [StringLength(20, ErrorMessage = " * Must be less than 20 characters")]
        public string CategoryofCard { get; set; }

        [StringLength(2, ErrorMessage = " * Must be less than 2 characters")]
        public string IssuingCountryISOA2Code { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
