using AlphaManagement.DAL.Entity.MasterData;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
    public class BDPolicePhoneBook:Base
    {
        public int? sectionId { get; set; }
        public Section section { get; set; }
        public int? departmentId { get; set; }
        public Department department { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        public string mobileNo { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        public string phoneNo { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string remarks { get; set; }
        public int? statusId { get; set; }
        public int? typeId { get; set; }
    }
}
