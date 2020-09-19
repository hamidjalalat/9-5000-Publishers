using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
   public class RegisterViewModel
    {
        [Display(Name ="شماره همراه")]
        [Required(ErrorMessage ="وارد کردن موبایل الزامی می باشد")]
        public string Mobile { get; set; }
        [Display(Name ="گذر واژه")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="وارد کردن رمز الزامی می باشد")]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage ="تعداد حروف وارد شده نبایید کمتر از 6 رقم باشد")]
        public string Password { get; set; }
        [Display(Name = "تکرار گذر واژه")]

        [DataType(DataType.Password),Compare(nameof(Password),ErrorMessage ="تکرار گذر واژه با گذر واژه یکسان نمی باشد")]
        public string ConfirmPassword { get; set; }

    }
}
