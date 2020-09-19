using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
   public class Movie:BaseEntity
    {
        public Movie() : base()
        {

        }
        [Display(Name = "شماره فیلم")]
        [Required(ErrorMessage = "وارد کردن شماره فیلم الزامی می باشد")]
        public int PageNumber { get; set; }

        [Display(Name = "انتخاب فایل فیلم")]
        [Required(ErrorMessage = "انتخاب فایل فیلم الزامی می باشد")]
        public string ServerPath { get; set; }
        public Guid PageBaseId { get; set; }
    }
}
