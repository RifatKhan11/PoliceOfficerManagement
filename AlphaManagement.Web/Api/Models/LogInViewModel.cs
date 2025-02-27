using System.ComponentModel.DataAnnotations;

namespace AlphaManagement.Web.Api.Models
{
    public class LogInViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} at most {1} characters long.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class ReturnObject
    {
        public object jwt { get; set; }
        public string otpCode { get; set; }
        public string message { get; set; }
        public string role { get; set; }
        public object userInfo { get; set; }
        public object employeeData { get; set; }
    }

    
}
