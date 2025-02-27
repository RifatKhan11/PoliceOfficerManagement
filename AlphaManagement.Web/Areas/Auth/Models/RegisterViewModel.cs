using AlphaManagement.DAL.Entity.Auth;
using AlphaManagement.Web.Areas.Auth.Models.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Auth.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} at most {1} characters long.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public string OldPassword { get; set; }
        
        //[EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string RoleId { get; set; }

        public string BPNo { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public int? UserId { get; set; }
        public string UserOTPCode { get; set; }
        public string verifyMessage { get; set; }
        public IEnumerable<ApplicationRoleViewModel> userRoles { get; set; }
        public RegisterLn fLang { get; set; }
        public UserInformation userInformation { get; set; }
        public string errorMsg { get; set; }
        public string UserName { get; set; }
        public int rankId { get; set; }
        public int? smsTypeId { get; set; }
    }
}
