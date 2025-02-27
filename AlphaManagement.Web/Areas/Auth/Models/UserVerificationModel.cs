using AlphaManagement.DAL.Entity.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Auth.Models
{
    public class UserVerificationModel
    {
        public int UserId { get; set; }
        public int? smsTypeId { get; set; }
        public string UserOTPCode { get; set; }
        public string errorMsg { get; set; }
        public UserInformation userInformation { get; set; }
    }
}
