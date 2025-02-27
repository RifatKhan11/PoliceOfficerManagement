using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfoHistories
{
   public class AddressInformationHistory:Base
    {
        public int? entryType { get; set; } //1=Admin;2=User

        public String UpdateUserId { get; set; }
        public ApplicationUser UpdateUser { get; set; }

        public int? addressInformationId { get; set; }
        public AddressInformation addressInformation { get; set; }

        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public int? spouseId { get; set; }
        public Spouse spouse { get; set; }

        public int? countryId { get; set; }
        public Country country { get; set; }
        public int? divisionId { get; set; }
        public Division division { get; set; }
        public int? districtId { get; set; }
        public District district { get; set; }
        public int? thanaId { get; set; }
        public Thana thana { get; set; }
        public int? unionWardId { get; set; }
        public UnionWard unionWard { get; set; }
        public int? villageId { get; set; }
        public Village village { get; set; }
        [Column(TypeName = "NVARCHAR(150)")]
        public string union { get; set; }
        [Column(TypeName = "NVARCHAR(150)")]
        public string postOffice { get; set; }
        [Column(TypeName = "NVARCHAR(50)")]
        public string postCode { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string blockSector { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string houseVillage { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string roadNumber { get; set; }
        [Column(TypeName = "NVARCHAR(250)")]
        public string addressDetails { get; set; }
        [Column(TypeName = "NVARCHAR(200)")]
        public string oneLineAddress { get; set; }
        //Type: Permamnent or Present

        [Column(TypeName = "NVARCHAR(50)")]
        public string type { get; set; }
    }
}
