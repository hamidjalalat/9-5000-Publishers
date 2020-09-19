using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Page : BaseEntity
    {
        public Page() : base()
        {

        }

        [Display(Name ="شماره صفحه")]
        [Required(ErrorMessage = "وارد کردن شماره صفحه الزامی می باشد")]
        public int PageNumber { get; set; }

        [Display(Name = "انتخاب فایل محتوا")]
        [Required(ErrorMessage = "انتخاب فایل محتوا الزامی می باشد")]

        public string ServerPath { get; set; }

        [Display(Name = "شماره فصل")]
        [Required(ErrorMessage = "وارد کردن شماره فصل الزامی می باشد")]
        public int SessionNumber { get; set; }

        [Display(Name = "کلمات کلیدی")]
        public string Content { get; set; }

        [Display(Name = "فعال")]
        public bool Enable { get; set; }

        [Display(Name = "نام کتاب")]
        public int BookId { get; set; }

        public virtual  Book Book { get; set; }

    }
}
