using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfoHistories;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.Web.Areas.Employee.Models.Lang;
using System;
using System.Collections.Generic;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class EmployeeInfoViewModel
    {
        public string employeeCode { get; set; }

        public string nationalID { get; set; }

        public string birthIdentificationNo { get; set; }

        public string govtID { get; set; }

        public string gpfNomineeName { get; set; }

        public string gpfAcNo { get; set; }

        public string nameEnglish { get; set; }

        public string nameBangla { get; set; }

        public string motherNameEnglish { get; set; }

        public string motherNameBangla { get; set; }

        public string fatherNameEnglish { get; set; }

        public string fatherNameBangla { get; set; }

        public string nationality { get; set; }

        public string disability { get; set; }

        public DateTime? dateOfBirth { get; set; }

        public DateTime? joiningDatePresentWorkstation { get; set; }

        public DateTime? joiningDateGovtService { get; set; }

        public DateTime? dateofregularity { get; set; }

        public DateTime? dateOfPermanent { get; set; }

        public DateTime? LPRDate { get; set; } //calculative From Date of Birth

        public DateTime? PRLStartDate { get; set; } //calculative From Date of Birth
        public DateTime? PRLEndDate { get; set; } //calculative From Date of Birth

        public string gender { get; set; }

        public string birthPlace { get; set; }

        public string maritalStatus { get; set; }

        public int? religionId { get; set; }

        public int? employeeTypeId { get; set; }

        public int? activityStatus { get; set; }

        public int? departmentId { get; set; }

        public string batch { get; set; }
        public string bloodGroup { get; set; }

        public bool freedomFighter { get; set; }
        public string freedomFighterNo { get; set; }

        public string telephoneOffice { get; set; }

        public string telephoneResidence { get; set; }

        public string pabx { get; set; }

        public string emailAddress { get; set; }

        public string emailAddressPersonal { get; set; } // Next generated not planned

        public string mobileNumberOffice { get; set; }

        public string mobileNumberPersonal { get; set; }

        public string specialSkill { get; set; }

        public string seniorityNumber { get; set; }

        public string designation { get; set; }

        public string skypeId { get; set; }

        public int? post { get; set; } // Related PostID But Not FK Referenced 

        public int designationCheck { get; set; }//Current Charged Checked

        public string joiningDesignation { get; set; }

        public string natureOfRequitment { get; set; } // Direct Or Absorbed

        public string homeDistrict { get; set; }
        public string sectionName { get; set; }

        public int? branchId { get; set; }

        public int? rankId { get; set; }
        public int? specialBranchUnitIdFinal { get; set; }

        public int? designationsId { get; set; }
        public int? SubbranchId { get; set; }

        public int? sectionId { get; set; }
        public string rationId { get; set; }
        public string drivingLicense { get; set; }

        //Application User LInk
        public String ApplicationUserId { get; set; }

        public int? isApproved { get; set; }//0= not approved,1=approved

        public virtual ICollection<EmployeeReport> EmployeeReports { get; set; }

        public IEnumerable<ApplicationRole> applicationRoles { get; set; }
        public IEnumerable<EmpViewModel> EmpDetailList { get; set; }
        public IEnumerable<Country> countries { get; set; }
        public IEnumerable<Section> section { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfoList { get; set; }
        public IEnumerable<EmployeeInfosViewModelFor_SP>  employeeInfosViewModelFor_SPs { get; set; }
        public IEnumerable<GetCheckedEmployeeList_SP>  employeeInfos_SPs { get; set; }
        public IEnumerable<UnitWiseEmployeeInfosViewModelFor_SP> UnitWiseEmployeeInfosViewModelFor_SP { get; set; }
        public IEnumerable<Designation> designations { get; set; }
        public IEnumerable<Rank> rank { get; set; }
        public IEnumerable<SpecialBranchUnit> branch { get; set; }
        public IEnumerable<ForeignTravel> foreignTravels { get; set; }
        public IEnumerable<Department> department { get; set; }
        public IEnumerable<EmployeeType> employeeTypes { get; set; }
        public IEnumerable<Religion> religions { get; set; }
        public IEnumerable<District> districts { get; set; }
        public IEnumerable<BCSBatch> bCSBatches { get; set; }
        public IEnumerable<PHQTRType> pHQTRTypes { get; set; }
        public IEnumerable<BadgeAndActivityModel> badgeAndActivityModels { get; set; }
        public IEnumerable<LevelofEducation> levelofEducations { get; set; }
        public IEnumerable<Organization> organizations { get; set; }
        public IEnumerable<Result> results { get; set; }
        public IEnumerable<StatusInfo> statusInfos { get; set; }
        public IEnumerable<Degree> degrees { get; set; }
        public IEnumerable<Division> Divisions { get; set; }
        public IEnumerable<Thana> thanas { get; set; }
        public IEnumerable<UnionWard> unionwards { get; set; }
        public IEnumerable<Section> sections { get; set; }
        public IEnumerable<Relation> relations { get; set; }
        public IEnumerable<Offense> offenses { get; set; }
        public IEnumerable<SpouseRelation> spouseRelations { get; set; }
        public IEnumerable<TrainingCategory> trainingCategories { get; set; }
        public IEnumerable<Assignment> assignments { get; set; }
        public IEnumerable<EmployeeReturnReason> employeeReturnReasons { get; set; }
        public IEnumerable<EducationalQualification> educationalQualifications { get; set; }
        public IEnumerable<AddressInformation> addressInformations { get; set; }
        public AddressInformation presentAddress { get; set; }
        public AddressInformation parmenantAddress { get; set; }
        public AddressInformation inLawsAddress { get; set; }
        public AddressInformation meternalAddress { get; set; }
        public IEnumerable<Spouse> spouses { get; set; }
        public Spouse spouse { get; set; }
        public IEnumerable<AwardEntry> awardEntries { get; set; }
        public IEnumerable<AwardEntry> awardEntryList { get; set; }
        public IEnumerable<Award> awardList { get; set; }
        public IEnumerable<PromotionLog> promotionLogs { get; set; }
        public IEnumerable<TraningLog> traningLogs { get; set; }
        public IEnumerable<DisciplinaryAction> disciplinaryActions { get; set; }
        public IEnumerable<AddressInformation> addressInformation { get; set; }
        public IEnumerable<NaturalPunishment> naturalPunishments { get; set; }
        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
        public IEnumerable<SpecialBranchUnit> specialBranchUnitsALL { get; set; }
        public IEnumerable<TrainingInstitute> trainingInstitutes { get; set; }
        public IEnumerable<Spouse> spousesInfo { get; set; }
        public IEnumerable<Banks> banks { get; set; }

        public IEnumerable<Rank> ranks { get; set; }
        public IEnumerable<SpecialBranchUnit> units { get; set; }
        public IEnumerable<BCSBatch>  bCSBatch { get; set; }
        public IEnumerable<EmployeeReportInfo>  employeeReportInfosSB { get; set; }
        public IEnumerable<EmployeeReportInfo>  employeeReportInfosBPA { get; set; }
        public IEnumerable<EmployeeMadicalInfo>  employeeMadicalInfos { get; set; }


        public IEnumerable<EducationalQualification> educationalQualification { get; set; }
        public IEnumerable<MedicalInfo> medicalInfos { get; set; }
        public IEnumerable<MedicalInfoViewModel> medicalInfo { get; set; }
        public IEnumerable<Disease> diseases { get; set; }
        public IEnumerable<Disease> diseasesList { get; set; }
        public IEnumerable<Vaccines> Vaccines { get; set; }
        public IEnumerable<ACRInformation> ACRInfo { get; set; }
        public IEnumerable<EmployeeListVM> employeeListVMs { get; set; }
        public IEnumerable<UserRoleVM> userRoles { get; set; }

        public ApplicationUser applicationUser { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public Photograph photograph { get; set; }
        public Photograph signature { get; set; }
        public AddressInformation presentAdd { get; set; }
        public AddressInformation permanentAdd { get; set; }
        public AddressInformation spouseAdd { get; set; }
        public AddressInformation maternalFamilyAdd { get; set; }
        public IEnumerable<GetPortfolioTransectionHistoryLog> TransectionHistoryLog { get; set; }

        //History Model
        public EmployeeInfoHistory employeeInfoHistory { get; set; }
        public IEnumerable<AssignmentHistory> assignmentHistories { get; set; }
        public IEnumerable<AwardEntryHistory> awardEntryHistories { get; set; }
        public IEnumerable<EducationalQualificationHistory> educationalQualificationHistories { get; set; }
        public IEnumerable<AddressInformationHistory> addressInformationHistories { get; set; }
        public IEnumerable<ForeignTravelHistory> foreignTravelHistories { get; set; }
        public IEnumerable<PromotionLogHistory> promotionLogHistories { get; set; }
        public IEnumerable<SpouseHistory> spouseHistories { get; set; }
        public IEnumerable<TraningLogHistory> traningLogHistories { get; set; }

        public EmployeeInfoLn fLang { get; set; }
        public AwardEntryLn fLangaward { get; set; }
        public DisciplinaryActionLn fLangDiciplinary { get; set; }
        public EducationalQualificationLn flangeducation { get; set; }
        public TraningLogLn fLangtraining { get; set; }

        //Family Information
        public int employeeId { get; set; }
        public int? spouseRelationId { get; set; }
        public string spouseName { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string spouseMaritalStatus { get; set; }
        public string spouseBloodGroup { get; set; }
        public string birthCertificat { get; set; }
        public string email { get; set; }
        public DateTime? spouseDateOfBirth { get; set; }
        public string occupation { get; set; }
        public string description { get; set; }
        public string mobile { get; set; }
        public int? designationId { get; set; }
        public DateTime? lastpromotionDate { get; set; }


        //JobHistory
        public string instituteName { get; set; }
        public string reasonofTransfer { get; set; }
        public string jobsectionName { get; set; }
        public DateTime? joiningDate { get; set; }
        public DateTime? resignDate { get; set; }
        public string servicePeriod { get; set; }

        // Education 
        public string levelOfEducation { get; set; }
        public int? degreeId { get; set; }
        public int? reldegreesubjectId { get; set; }
        public int? organizationId { get; set; }
        public int? resultId { get; set; }
        public int? passingYear { get; set; }
        public string institution { get; set; }

        //Training 
        public string trainingType { get; set; }
        public string trainingCategory { get; set; }
        public string trainingName { get; set; }
        public string trainingInstitute { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }

        //Award
        public string awardName { get; set; }
        public string purpose { get; set; }
        public DateTime? awardDate { get; set; }
        public string status { get; set; }
        //disciplinary

        //disciplinary
        public int empDisciplinaryId { get; set; }
        public int? offenseId { get; set; }
        public int? naturalPunishmentId { get; set; }
        public DateTime? dispunishmentDate { get; set; }
        public DateTime? punishmentstartingDate { get; set; }
        public DateTime? punishmentendingDate { get; set; }
        public string remark { get; set; }
        //public string remarks { get; set; }

    }


    public class EmployeePortfolioDashBoardViewModel
    {
        public int? totalRegistration { get; set; }
        public int? onGoing { get; set; }
        public int? registration { get; set; }
        public int? finalSubmit { get; set; }
        public int? varified { get; set; }
        public int? registred { get; set; }
        public int? returned { get; set; }
        public int? checkedItem { get; set; }
        public int? pendingItem { get; set; }
        public int? todaycheckedItem { get; set; }

        //internal Posting
        public int? internalongoing { get; set; }
        public int? internalApproved { get; set; }
        public int? internalReturn { get; set; }
        public int? internalDraft { get; set; }
        public int? overduejoining { get; set; }

        public List<UnitWiseEmployeeCountModel> unitList { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfoList { get; set; }
        public IEnumerable<EmployeeInfosViewModelFor_SP> employeeInfosViewModelFor_SPs { get; set; }

        public int? todaysTotalRegistration { get; set; }
        public int? todaysOnGoing { get; set; }
        public int? todaysRegistration { get; set; }
        public int? todaysFinalSubmit { get; set; }
        public int? todaysVarified { get; set; }
        public int? todaysRegistred { get; set; }
        public int? todaysReturned { get; set; }

        public string[] employeeCodes { get; set; }
        public int?[] isCheck { get; set; }
        public int?[] isEditCheck { get; set; }
    }

}
