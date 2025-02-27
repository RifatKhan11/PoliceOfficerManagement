using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfoHistories
{
    public class EmployeeGradation:Base
    {
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string employeeCode { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string nameEnglish { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string nameBangla { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfBirth { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? joiningDateGovtService { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? firstPromotionDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? lastPromotionDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? joiningDatePresentWorkstation { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? commentDate { get; set; }
        public DateTime? LPRDate { get; set; } //calculative From Date of Birth

        public int? joiningRankId { get; set; }
        public Rank joiningRank { get; set; }

        public int? branchId { get; set; }
        public virtual SpecialBranchUnit branch { get; set; }

        public int? rankId { get; set; }
        public Rank rank { get; set; }
        public int? designationsId { get; set; }
        public Designation designations { get; set; }
        public int? sectionId { get; set; }
        public Section section { get; set; }
        public int? bCSBatchId { get; set; }
        public BCSBatch bCSBatch { get; set; }
        public int? bcsPosition { get; set; }
        [Column(TypeName = "nvarchar(350)")]
        public string experienceName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? expReqDate { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string educationQualification { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string title { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string homeDistrict { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string imageUrl { get; set; }
        public int? organizationId { get; set; }
        public Organization organization { get; set; }
        public string SpouseAddress { get; set; }
        public string regularJoin { get; set; }
        


        //Militery Officer Info
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? commissionReceiptDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? commissionLPRDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? civilReqDate { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string civilRank { get; set; }
        [Column(TypeName = "nvarchar(350)")]
        public string comments { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string firstAdhoc { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string secondAdhoc { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string thirdAdhoc { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string forthAdhoc { get; set; }
        public int? gradationSerial { get; set; }
        public int? statusId { get; set; }


        public int? degreeId { get; set; }
        public Degree degree { get; set; }

        //Spouse Address
        public int? SpDivisionId { get; set; }
        public Division SpDivision { get; set; }

        public int? SpDistrictId { get; set; }
        public District SpDistrict { get; set; }

        public int? SpThanaId { get; set; }
        public Thana SpThana { get; set; }
        public string gender { get; set; }

    }
}
