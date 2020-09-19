using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
   public class GoldenTip:BaseEntity
    {
        public GoldenTip() : base()
        {

        }
        [Display(Name = "شماره صفحه")]
        [Required(ErrorMessage = "وارد کردن شماره صفحه الزامی می باشد")]
        public int PageNumber { get; set; }
        [Display(Name = "انتخاب فایل نکات طلایی")]
        [Required(ErrorMessage = "انتخاب فایل نکات طلایی الزامی می باشد")]
        public string ServerPath { get; set; }
        public Guid PageBaseId { get; set; }
    }
}
