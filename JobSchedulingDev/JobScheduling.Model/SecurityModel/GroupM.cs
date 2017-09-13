using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace JobScheduling.Entity.SecurityModel
{
    public class GroupM
    {
        public string GroupID { get; set; }

        [Required(ErrorMessage = " * Required")]
        [RegularExpression("[^<]*<?[^a-zA-Z]*", ErrorMessage = " * The format is not correct!")]
        [StringLength(50, ErrorMessage = " * Must be less than 50 characters")]
        public string GroupName { get; set; }

        public string Operation { get; set; }
        public string SystemID { get; set; }

        [StringLength(400, ErrorMessage = " * Must be less than 400 characters")]
        [RegularExpression("[^<]*<?[^a-zA-Z]*", ErrorMessage = " * The format is not correct!")]
        public string Description { get; set; }

        public int NumOfUsers { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public byte IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime DeletedTime { get; set; }
    }
}
