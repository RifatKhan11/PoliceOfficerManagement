using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PoliceOfficerManagement.Data
{
    public class ApplicationUser : IdentityUser
    {
        public int? userTypeId { get; set; }
        //public UserType userType { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(300)]
        public string ImagePath { get; set; }
        public string otpCode { get; set; }
        public string emailOtpCode { get; set; }
        public DateTime? otpExpire { get; set; }
        public DateTime? emailVerifiedDate { get; set; }
        public int? isVarified { get; set; }
        public int? isChangePassword { get; set; }

        public int? isActive { get; set; }
        public int? isDelete { get; set; }      

        public DateTime? createdAt { get; set; }
        [MaxLength(120)]
        public string createdBy { get; set; }

        public DateTime? updatedAt { get; set; }
        [MaxLength(120)]
        public string updatedBy { get; set; }
       
    }
}
