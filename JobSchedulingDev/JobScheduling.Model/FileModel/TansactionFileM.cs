using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace JobScheduling.Model.FileModel
{
    public class TransactionFileM
    {
        public string Region { get; set; }

        [StringLength(7, ErrorMessage = " * Must be less than 7 characters")]
        [Required(ErrorMessage = " * Required")]
        public string SerialNo { get; set; }
        

        [StringLength(6, ErrorMessage = " * Must be less than 6 characters")]
        [Required(ErrorMessage = " * Required")]
        public string StoreCode { get; set; }


        [RegularExpression(@"((^((1[8-9]\d{2})|([2-9]\d{3}))(10|12|0?[13578])(3[01]|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))(11|0?[469])(30|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))(0?2)(2[0-8]|1[0-9]|0?[1-9])$)|(^([2468][048]00)(0?2)(29)$)|(^([3579][26]00)(0?2)(29)$)|(^([1][89][0][48])(0?2)(29)$)|(^([2-9][0-9][0][48])(0?2)(29)$)|(^([1][89][2468][048])(0?2)(29)$)|(^([2-9][0-9][2468][048])(0?2)(29)$)|(^([1][89][13579][26])(0?2)(29)$)|(^([2-9][0-9][13579][26])(0?2)(29)$))", ErrorMessage = " * Time format is invalid")]
        [StringLength(8,MinimumLength=8, ErrorMessage = " * Must be less than 8 characters")]
        public string TransactionDate { get; set; }
        

        [Required(ErrorMessage = " * Required")]
        [StringLength(10, ErrorMessage = " * Must be less than 10 characters")]
        public string DocumentNo { get; set; }


        public string CreditCardNo { get; set; }
        
        

        public string BaseAmount { get; set; }

        public string BIN { get; set; }


        public string CardBrand { get; set; }
        public string IssuingBank { get; set; }
        public string TypeofCard { get; set; }
        public string CategoryofCard { get; set; }
        public string IssuingCountryCode { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
