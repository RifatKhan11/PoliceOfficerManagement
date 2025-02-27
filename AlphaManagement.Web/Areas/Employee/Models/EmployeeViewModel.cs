using AlphaManagement.DAL.Entity.MasterData;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class EmployeeViewModel
    {
        public int empId { get; set; }
        public string title { get; set; }
        public string nameBangla { get; set; }
        public string nameEnglish { get; set; }
        public string bpNo { get; set; }

        public int? rankId { get; set; }
        public int? PhqCountry { get; set; }
        public int? PhqTrType { get; set; }
        public int? attachmentBranchId { get; set; }
        public int? specialSkillTypeId { get; set; }
        public int? empTypeId { get; set; }
        public string designation { get; set; }
        public string currentWorkStation { get; set; }
        public DateTime? joiningDatePresentWorkstation { get; set; }
        public string fatherNameEnglish { get; set; }
        public string fatherNameBangla { get; set; }
        public string motherNameEnglish { get; set; }
        public string motherNameBangla { get; set; }
        public string spouseName { get; set; }
        public string gender { get; set; }
        public string nationalID { get; set; }
        public string homeDistrict { get; set; }
        public string spouseHomeDistrict { get; set; }
        public string permanentAddress { get; set; }
        public string presentAddress { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public DateTime? joiningDateGovtService { get; set; }
        public DateTime? lprDate { get; set; }
        public DateTime? policejoiningrank { get; set; }
        public DateTime? dateofjoining { get; set; }
        public DateTime? retirementdate { get; set; }
        public string batch { get; set; }
        public string retiredDate { get; set; }
        public string servicePeriod { get; set; }
        public string joiningDesignation { get; set; }
        public string bloodGroup { get; set; }
        public int? gradationSerial { get; set; }
        public decimal? height { get; set; }
        public decimal? weight { get; set; }
        public string identificationSign { get; set; }
        public int? religionId { get; set; }
        public string maritalStatus { get; set; }
        public string tribal { get; set; }

        public int? tribals { get; set; }
        public int? bcsPosition { get; set; }
        public int? departmentalPromotionYear { get; set; }
        public int? sectionId { get; set; }

        public string mobileNumberOffice { get; set; }
        public string mobileNumberPersonal { get; set; }
        public string emailOffice { get; set; }
        public string emailPersonal { get; set; }
        public string promotionDate { get; set; }
        public string status { get; set; }
        public string passportNo { get; set; }
        public string telephoneOffice { get; set; }
        public int? salaryBankId { get; set; }
        public string salaryAccountNo { get; set; }
        public int? otherBankId { get; set; }
        public string otherBankAccountNo { get; set; }
        public string remarks { get; set; }
        public string linkdInId { get; set; }
        public string facebookId { get; set; }
        public string skypeId { get; set; }
        public string Other { get; set; }
        public string OtherTrainingCat { get; set; }
        public string OtherOrganization { get; set; }
        public string skill { get; set; }
        public string extraActivity { get; set; }
        public string extraActivitys { get; set; }
        public string extraSkill { get; set; }
        public string sectionName { get; set; }

  
        public string familyInformation { get; set; }
        public string jobHistroy { get; set; }
        public string educationInformation { get; set; }
        public string trainingInformation { get; set; }
        public string awardInformation { get; set; }
        public string disciplinaryInformation { get; set; }
        public string select { get; set; }
        public string rationId { get; set; }
        public string drivingLicense { get; set; }
        public int? designationsId { get; set; }
        public DateTime? lastpromotionDate { get; set; }
        public DateTime? joiningDateGovt { get; set; }
        


        //Family Information
        public int spouseId { get; set; }
        public int employeeId { get; set; }
        public int? spouseRelationId { get; set; }
        public string Name { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string spouseMaritalStatus { get; set; }
        public string spouseBloodGroup { get; set; }
        public string birthCertificate { get; set; }
        public string email { get; set; }
        public DateTime? spouseDateOfBirth { get; set; }
        public string occupation { get; set; }
        public string description { get; set; }
        public string mobile { get; set; }
        public int? bCSBatchId { get; set; }
        public int? spousedistrictId { get; set; }


        //JobHistory
        public int empJobHistoryId { get; set; }
        public int? secId { get; set; }
        public int? ranksId { get; set; }
        public int? specialBranchUnitId { get; set; }
        public string instituteName { get; set; }
        public string reasonofTransfer { get; set; }
        public string jobsectionName { get; set; }
        public DateTime? joiningDate { get; set; }
        public DateTime? resignDate { get; set; }
       // public string servicePeriod { get; set; }

        // Education 
        public int empEducationId { get; set; }
        public string levelOfEducation { get; set; }
        public int? degreeId { get; set; }
        public int? reldegreesubjectId { get; set; }
        public int? organizationId { get; set; }
        public int? resultId { get; set; }
        public int? passingYear { get; set; }
        public string institution { get; set; }
        public string gradeGPA { get; set; }
        public string gradeClass { get; set; }
        public int? educationCountryId { get; set; }
        //Training 
        public string trainingTypes { get; set; }
        public int empTrainingId { get; set; }
        public int? trainingCategoryId { get; set; }
        public string trainingName { get; set; }
        public int? trainingInstituteId { get; set; }
        public int? trainingCountryId { get; set; }
        public string referenceNum { get; set; }
        public string trDuration { get; set; }
        public DateTime? disDuration { get; set; }
        public string fortrainingInstitute { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }

        //Award
        public int empAwardId { get; set; }
        public string awardName { get; set; }
        public string otherAwardName { get; set; }
        public string purpose { get; set; }
        public DateTime? awardDate { get; set; }
      //  public string status { get; set; }
        
            
            
       //disciplinary
        public int disciplinaryId { get; set; }
        public int? offenseId { get; set; }
        public int? naturalPunishmentId { get; set; }
        public DateTime? dispunishmentDate { get; set; }
        public DateTime? punishmentstartingDate { get; set; }
        public DateTime? punishmentendingDate { get; set; }
        public string remark { get; set; }
        //public string remarks { get; set; }

        //Address
        public string addressType { get; set; }
        public int? divisionId { get; set; }
        public int? districtId { get; set; }
        public int? thanaId { get; set; }
        public int? unionId { get; set; }
        public string postCode { get; set; }
        public string AddressDetails { get; set; }
        public int? sameParmanentAddress { get; set; }

        //Promotion
        public int addressId { get; set; }
        public int promotionId { get; set; }
        public int? promotionrankId { get; set; }
        public DateTime? PromotionjoiningDate { get; set; }
        public DateTime? PromotionDate { get; set; }

        //Medical
        public int? diseaseId { get; set; }
        public int empMedicalId { get; set; }

        public DateTime? date { get; set; }
        public DateTime? recoverydate { get; set; }
        public DateTime? checkUpDate { get; set; }
        public DateTime? vaccinatedDate { get; set; }
        public string isHospitalised { get; set; }
        public string isDelete { get; set; }
        public string isVaccinated { get; set; }
        public string hospitalName { get; set; }
        public string diseaseName { get; set; }
        public string referanceDoctor { get; set; }
        public string CVRMedicalInjury { get; set; }
        public string lastCheckupHistory { get; set; }
        public string year { get; set; }
        public int? satatus { get; set; }
        public int?[] diseaseIds { get; set; }
        public int?[] vaccineIds { get; set; }
        public int[] medicalIdArr { get; set; }
        public int?[] diseaseIdArr { get; set; }
        public int[] isHospitalisedArr { get; set; }
        public int[] isUnderArr { get; set; }
        public int[] isVaccinatedArr { get; set; }
        public DateTime?[] vaccinatedDateArr { get; set; }
        public string[] yearArr { get; set; }
        public string[] observationArr { get; set; }
        public IFormFile photo { get; set; }
        public string photoPictures { get; set; }

    }

    public class GroupSubjectViewModel
    {
        public int? SdegreeId { get; set; }
        public string SName { get; set; }
    }
}
