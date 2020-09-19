using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class LoginViewModel
    {
        [Display(Name ="نام کاربری")]
        public string Mobile { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "گذر واژه")]
        public string Password { get; set; }

        [Display(Name ="بخاطر بسپار")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
