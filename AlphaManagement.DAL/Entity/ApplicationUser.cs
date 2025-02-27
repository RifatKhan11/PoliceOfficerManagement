using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AlphaManagement.DAL.Entity.Auth;

namespace AlphaManagement.DAL.Entity
{
    public class ApplicationUser: IdentityUser
    {
        public int? isActive { get; set; }

        public DateTime? createdAt { get; set; }
        [MaxLength(120)]
        public string createdBy { get; set; }

        public DateTime? updatedAt { get; set; }
        [MaxLength(120)]
        public string updatedBy { get; set; }
        [MaxLength(20)]
        public string bpNo { get; set; }
        [MaxLength(10)]
        public string otpCode { get; set; }
        [NotMapped]
        public string Token { get; set; }

        [NotMapped]
        public string Role { get; set; }
    }
}
