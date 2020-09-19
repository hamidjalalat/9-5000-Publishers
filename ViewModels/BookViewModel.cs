using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
    public class BookViewModel
    {
        public BookViewModel() : base()
        {

        }
        public int Id { get; set; }
        [Display(Name = "نام کتاب ")]
        public string Name { get; set; }

        [Display(Name = "انتخاب فایل محتوا ")]
        public string Content { get; set; }

        [Display(Name = " جلد کتاب ")]
        public string Cover { get; set; }

        [Display(Name = "نویسنده ")]
        [Required(ErrorMessage = "انتخاب نویسنده الزامی می باشد")]

        public string Author { get; set; }

        [Display(Name = "ناشر")]
        [Required(ErrorMessage = "انتخاب ناشر الزامی می باشد")]

        public string Publisher { get; set; }

        [Display(Name = "تاریخ شروع تالیف")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "تاریخ خاتمه تالیف")]
        public DateTime EndCreateDate { get; set; }

        [Display(Name = "تاریخ شروع تالیف")]
        public string CreateDateShamsi { get; set; }

        [Display(Name = "تاریخ خاتمه تالیف")]
        public string EndCreateDateShamsi { get; set; }

        [Display(Name = "تعداد صفحات ")]
        public int ContPage { get; set; }

        [Display(Name = "نوع کتاب")]
        public int BookTypeId { get; set; }

        [Display(Name = "نوع رشته تحصیلی")]
        public int FieldOfStudyId { get; set; }

        [Display(Name = "مقطع")]
        public int SectionId { get; set; }

        [Display(Name = "نوع محصول")]
        public string ProductName { get; set; }

        [Display(Name = "نام درس")]
        public int LessonId { get; set; }
        [Display(Name = "فعال")]
        public bool Enable { get; set; }
    }
}
