using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
   public class ConceptualPoint:BaseEntity
    {
        public ConceptualPoint() : base()
        {

        }
        [Display(Name = "شماره صفحه")]
        [Required(ErrorMessage = "وارد کردن شماره صفحه الزامی می باشد")]
        public int PageNumber { get; set; }
        [Display(Name = "انتخاب فایل نکات مفهومی")]
        [Required(ErrorMessage = "انتخاب فایل نکات مفهومی الزامی می باشد")]
        public string ServerPath { get; set; }
        public Guid PageBaseId { get; set; }
    }
}
