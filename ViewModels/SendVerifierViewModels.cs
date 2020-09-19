using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
  public  class SendVerifierViewModels
    {
        public SendVerifierViewModels() : base()
        {

        }
        [Display(Name ="شماره همراه")]
        [Required(ErrorMessage ="لطفا شماره موبایل را وارد کنید")]
        [RegularExpression(pattern: "09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}",
        ErrorMessage = "شماره موبایل معتبر نمی باشد")]
        public string Mobile { get; set; }
    }
}
