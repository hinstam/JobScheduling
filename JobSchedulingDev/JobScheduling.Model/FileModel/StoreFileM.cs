using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace JobScheduling.Model.FileModel
{
    public class StoreFileM
    {

        public Guid StoreID { get; set; }

         [Required(ErrorMessage = " * Required")]
        public Guid DistrictID { get; set; }

         [Required(ErrorMessage = " * Required")]
         [StringLength(6, ErrorMessage = " * Must be less than 6 characters")]
         public string StoreCode { get; set; }

        //[StringLength(6, ErrorMessage = " * Must be less than 6 characters")]
        public string DistrictCode { get; set; }

        [StringLength(10, ErrorMessage = " * Must be less than 10 characters")]
        public string StoreFirstDescription { get; set; }

        [StringLength(50, ErrorMessage = " * Must be less than 50 characters")]
        public string StoreLastDescription { get; set; }

        [StringLength(255, ErrorMessage = " * Must be less than 255 characters")]
        public string Address { get; set; }

        public string DistrictDescription { get; set; }


        public Guid CountryID { get; set; }

        public string CountryCode { get; set; }
        public string CountryDescription { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
