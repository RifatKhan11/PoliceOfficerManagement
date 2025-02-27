using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.EmployeeInfoHistories
{
   public class EmployeeInfoHistory:Base
    {
        public int? entryType { get; set; } //1=Admin;2=User

        public String UpdateUserId { get; set; }
        public ApplicationUser UpdateUser { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string employeeCode { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string nationalID { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string birthIdentificationNo { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string govtID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string gpfNomineeName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string gpfAcNo { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string nameEnglish { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string nameBangla { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string motherNameEnglish { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string motherNameBangla { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string fatherNameEnglish { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string fatherNameBangla { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string nationality { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string disability { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string servicePeriod { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string salaryAccountNo { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string bankAccount { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string otherBankAccountNo { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string passportNo { get; set; }
        [Column(TypeName = "nvarchar(350)")]
        public string facebookId { get; set; }
        [Column(TypeName = "nvarchar(350)")]
        public string linkdInId { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string tribal { get; set; }
        [Column(TypeName = "nvarchar(350)")]
        public string identificationSign { get; set; }
        public decimal? height { get; set; }
        public decimal? weight { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfBirth { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? joiningDatePresentWorkstation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? joiningDateGovtService { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateofregularity { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfPermanent { get; set; }

        public DateTime? LPRDate { get; set; } //calculative From Date of Birth

        public DateTime? PRLStartDate { get; set; } //calculative From Date of Birth
        public DateTime? PRLEndDate { get; set; } //calculative From Date of Birth
        public DateTime? promotionDate { get; set; }
        [MaxLength(10)]
        public string gender { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string birthPlace { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string maritalStatus { get; set; }

        public int? religionId { get; set; }
        public Religion religion { get; set; }

        public int? employeeTypeId { get; set; }
        public EmployeeType employeeType { get; set; }

        public int? activityStatus { get; set; }

        public int? departmentId { get; set; }
        public Department department { get; set; }


        public string batch { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string bloodGroup { get; set; }

        public bool freedomFighter { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string freedomFighterNo { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string telephoneOffice { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string telephoneResidence { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string pabx { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string emailAddress { get; set; }

        [Column(TypeName = "nvarchar(50)")]

        public string emailAddressPersonal { get; set; } // Next generated not planned

        [Column(TypeName = "nvarchar(50)")]
        public string mobileNumberOffice { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string mobileNumberPersonal { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string specialSkill { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string seniorityNumber { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string designation { get; set; }
        [Column(TypeName = "nvarchar(350)")]
        public string skypeId { get; set; }          //Twitter_Id

        public int? post { get; set; } // Related PostID But Not FK Referenced 

        public int designationCheck { get; set; }//Current Charged Checked
        [Column(TypeName = "nvarchar(250)")]
        public string joiningDesignation { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string natureOfRequitment { get; set; } // Direct Or Absorbed
        [Column(TypeName = "nvarchar(150)")]
        public string homeDistrict { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string drivingLicense { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string rationId { get; set; }

        public int? branchId { get; set; }
        public virtual SpecialBranchUnit branch { get; set; }

        public int? banksId { get; set; }
        public Banks banks { get; set; }

        public int? otherBanksId { get; set; }
        public Banks otherBanks { get; set; }

        public int? pHQTRTypeId { get; set; }
        public PHQTRType pHQTRType { get; set; }

        public int? attachmentBranchId { get; set; }
        public virtual SpecialBranchUnit attachmentBranch { get; set; }

        public int? countryId { get; set; }
        public Country country { get; set; }

        public int? rankId { get; set; }
        public Rank rank { get; set; }
        public int? designationsId { get; set; }
        public Designation designations { get; set; }
        public int? sectionId { get; set; }
        public Section section { get; set; }
        public int? bCSBatchId { get; set; }
        public BCSBatch bCSBatch { get; set; }
        public int? bcsPosition { get; set; }
        public int? departmentalPromotionYear { get; set; }
        [Column(TypeName = "nvarchar(350)")]
        public string skill { get; set; }
        [Column(TypeName = "nvarchar(350)")]
        public string extraActivity { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string sectionName { get; set; }
        public string extraSkill { get; set; }
        public string extraActivitys { get; set; }
        //Application User LInk
        public String ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int? isApproved { get; set; }//0= not approved,1=approved

        public virtual ICollection<Photograph> Photographs { get; set; }
        public virtual ICollection<AddressInformation> AddressInformation { get; set; }
    }
}
