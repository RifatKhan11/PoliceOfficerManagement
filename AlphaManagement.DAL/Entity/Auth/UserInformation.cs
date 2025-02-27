using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.Auth
{
    public class UserInformation:Base
    {
        [Column(TypeName = "nvarchar(100)")]
        public string userName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string email { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string password { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string otpCode { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string userRole { get; set; }
        public int? isVerified { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string phoneNumber { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string bpNo { get; set; }
        public int? statusId { get; set; }
    }
}
