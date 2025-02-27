using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.EmployeeInfoHistories
{
    public class EmployeeTransectionLog:Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string remarks { get; set; }

        public int? statusInfoId { get; set; }
        public StatusInfo statusInfo { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string empName { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string nextEmpName { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Status { get; set; }
    }
}
