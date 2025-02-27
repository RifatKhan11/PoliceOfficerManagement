using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.ApprovalMatrix;
using AlphaManagement.DAL.Entity.Auth;
using AlphaManagement.DAL.Entity.EmployeeInfoHistories;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.InternalPosting;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Entity.Organogram;
using AlphaManagement.DAL.Models;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.DAL
{
    public class AlphaDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AlphaDbContext(DbContextOptions<AlphaDbContext> options, IHttpContextAccessor _httpContextAccessor) : base(options)
        {
            this._httpContextAccessor = _httpContextAccessor;
        }

        #region Auth
        public DbSet<UserAccessPage> UserAccessPages { get; set; }
        public DbSet<ModuleAccessPage> ModuleAccessPages { get; set; }
        public DbSet<AlphaModule> AlphaModules { get; set; }
        public DbSet<Navbar> Navbars { get; set; }
        public DbSet<UserLogHistory> UserLogHistories { get; set; }
        public DbSet<UserInformation> UserInformation { get; set; }
        public DbSet<UnauthorizeUserLog> unauthorizeUserLogs { get; set; }
        public DbSet<MobileDeviceInformation> MobileDeviceInformation { get; set; }
        #endregion

        #region Master Data
        public DbSet<ActivityStatus> ActivityStatuses { get; set; }
        public DbSet<CourseTitle> CourseTitles { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LevelofEducation> LevelofEducations { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<RelDegreeSubject> RelDegreeSubjects { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SpecialBranchUnit> SpecialBranchUnits { get; set; }
        public DbSet<SubBranchUnit> SubBranchUnits { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TrainingCategory> TrainingCategories { get; set; }
        public DbSet<TrainingInstitute> TrainingInstitutes { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<StatusInfo> StatusInfos { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Spouse> Spouses { get; set; }
        public DbSet<SpouseRelation> SpouseRelations { get; set; }
        public DbSet<NaturalPunishment> NaturalPunishments { get; set; }
        public DbSet<Offense> Offenses { get; set; }
        public DbSet<SalaryGrade> SalaryGrades { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<OtherQualificationHead> OtherQualificationHeads { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<MailLog> MailLogs { get; set; }
        public DbSet<BCSBatch> BCSBatches { get; set; }
        public DbSet<Banks> Banks { get; set; }
        public DbSet<Vaccines> Vaccines { get; set; }
        public DbSet<VaccineGroup> VaccineGroups { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<DiseaseGroup> DiseaseGroups { get; set; }
        public DbSet<AnulipiList> AnulipiLists { get; set; }
        public DbSet<SpecialSkillType> specialSkillTypes { get; set; }
        public DbSet<PriorityLevelType> priorityLevelTypes { get; set; }
        public DbSet<PostingPriorityLevel> postingPriorityLevels { get; set; }
        public DbSet<MedicalMainCategory> medicalMainCategories { get; set; }
        public DbSet<MedicalSubCategory> medicalSubCategories { get; set; }
        public DbSet<EmployeeMadicalInfo> employeeMadicalInfos { get; set; }
        public DbSet<EmployeeReportInfo> employeeReportInfos { get; set; }
        
        #endregion

        #region Address Data
        public DbSet<AddressCategory> AddressCategories { get; set; }
        public DbSet<AddressInformation> AddressInformation { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<DivisionDistrict> DivisionDistricts { get; set; }
        public DbSet<MetropolitanArea> MetropolitanAreas { get; set; }
        public DbSet<NationalIdentityType> NationalIdentityTypes { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<PoliceSubUnit> PoliceSubUnits { get; set; }
        public DbSet<PoliceThana> PoliceThanas { get; set; }
        public DbSet<PoliceUnit> PoliceUnits { get; set; }
        public DbSet<PostOffice> PostOffices { get; set; }
        public DbSet<RangeMetro> RangeMetros { get; set; }
        public DbSet<Thana> Thanas { get; set; }
        public DbSet<UnionWard> UnionWards { get; set; }
        public DbSet<Village> Villages { get; set; }
        public DbSet<ZoneCircle> ZoneCircles { get; set; }
        public DbSet<DivisionInfo> DivisionInfos { get; set; }
        public DbSet<DistrictInfo> DistrictInfos { get; set; }
        public DbSet<UpazilaInfo> UpazilaInfos { get; set; }
        public DbSet<UnionInfo> UnionInfos { get; set; }
        public DbSet<PostInUnit> PostInUnits { get; set; }
        #endregion

        #region Employee Infoes
        public DbSet<Assignment> Assignments{ get; set; }
        public DbSet<AssignmentMaster> AssignmentMasters{ get; set; }
        public DbSet<CancelAssignment> CancelAssignments{ get; set; }
        public DbSet<AwardEntry> AwardEntries{ get; set; }
        public DbSet<DisciplinaryAction> DisciplinaryActions{ get; set; }
        public DbSet<EducationalQualification> EducationalQualifications{ get; set; }
        public DbSet<EmployeeInfo> EmployeeInfos{ get; set; }
        public DbSet<PromotionLog> PromotionLogs{ get; set; }
        public DbSet<TraningLog> TraningLogs{ get; set; }
        public DbSet<Photograph> Photographs{ get; set; }
        public DbSet<ForeignTravel> ForeignTravels{ get; set; }
        public DbSet<MedicalInfo> MedicalInfos{ get; set; }
        public DbSet<EnlistedAssignment> EnlistedAssignments{ get; set; }
        public DbSet<BankInformation> BankInformation{ get; set; }
        public DbSet<ACRInformation> ACRInformation{ get; set; }
        public DbSet<EmployeeMedicalDisease> EmployeeMedicalDiseases{ get; set; }
        public DbSet<EmployeeMedicalVaccine> EmployeeMedicalVaccines{ get; set; }
        public DbSet<AssignmentAnulipi> AssignmentAnulipis{ get; set; }
        public DbSet<AssignmentAnulipiPreview> AssignmentAnulipiPreviews{ get; set; }
        public DbSet<AssignmentSignature> AssignmentSignatures{ get; set; }
        public DbSet<EnlistedAssignmentMaster> EnlistedAssignmentMasters{ get; set; }
        public DbSet<AssignmentStatusLog> AssignmentStatusLogs{ get; set; }
        public DbSet<EmployeeReturnReason> EmployeeReturnReasons{ get; set; }
        public DbSet<PHQTRType> PHQTRTypes{ get; set; }
        public DbSet<BDPolicePhoneBook> BDPolicePhoneBooks{ get; set; }

        public DbQuery<SuggestedUnitModel> SuggestedUnits { get; set; }
        public DbQuery<GlobalSearchModel> globalSearchModels { get; set; }
        public DbQuery<GetPortfolioTransectionHistoryLog> transectionHistoryLogs { get; set; }
        public DbQuery<EmployeePercentWisePrograssModel> employeePercentWisePrograsses { get; set; }
        public DbQuery<LPRDateModel> lPRDates { get; set; }
        public DbQuery<BadgeAndActivityModel> badgeAndActivityModels { get; set; }
        public DbQuery<PIMSDataModel> pIMSDataModels { get; set; }

        public DbSet<PhoneBookRank> PhoneBookRanks { get; set; }
        public DbSet<PhoneBookUnit> PhoneBookUnits { get; set; }
        public DbSet<PhoneBook> PhoneBooks { get; set; }

        #endregion

        #region Internal Posting
        public DbSet<InternalEnlistedAssignmentMaster> InternalEnlistedAssignmentMasters { get; set; }
        public DbSet<InternalAssignmentType> InternalAssignmentTypes { get; set; }
        public DbSet<InternalAnulipiList> InternalAnulipiLists { get; set; }
        public DbSet<InternalAssignment> InternalAssignments { get; set; }
        public DbSet<InternalAssignmentAnulipi> InternalAssignmentAnulipis { get; set; }
        public DbSet<InternalAssignmentMaster> InternalAssignmentMasters { get; set; }
        public DbSet<InternalAssignmentStatusLog> InternalAssignmentStatuses { get; set; }
        public DbSet<InternalCancelAssignment> InternalCancelAssignments { get; set; }
        public DbSet<InternalEnlistedAssignment> InternalEnlistedAssignments { get; set; }
        public DbSet<InternalApprovalLog> InternalApprovalLogs { get; set; }
        public DbSet<InternalAssignmentTransectionLog> InternalAssignmentTransectionLogs { get; set; }
        #endregion

        #region Employee EmployeeInfoHistory
        public DbSet<EmployeeGradation> EmployeeGradations{ get; set; }
        public DbSet<EmployeeGradationHistory> EmployeeGradationHistories{ get; set; }
        public DbSet<EmployeeInfoHistory> EmployeeInfoHistories{ get; set; }
        public DbSet<AddressInformationHistory> AddressInformationHistories{ get; set; }
        public DbSet<AssignmentHistory> AssignmentHistories{ get; set; }
        public DbSet<AssignmentTransectionLog> AssignmentTransectionLogs{ get; set; }
        public DbSet<PromotionLogHistory> PromotionLogHistories{ get; set; }
        public DbSet<EducationalQualificationHistory> EducationalQualificationHistories{ get; set; }
        public DbSet<TraningLogHistory> TraningLogHistories{ get; set; }
        public DbSet<AwardEntryHistory> AwardEntryHistories{ get; set; }
        public DbSet<ForeignTravelHistory> ForeignTravelHistories{ get; set; }
        public DbSet<SpouseHistory> SpouseHistories{ get; set; }
        public DbSet<EmployeeTransectionLog> EmployeeTransectionLogs{ get; set; }
        public DbSet<EmployeePrintHistoryLog> employeePrintHistoryLogs{ get; set; }

        #endregion

        #region Approval Matrix
        public DbSet<MatrixType> MatrixTypes { get; set; }
        public DbSet<ApprovalMatrix> ApprovalMatrices { get; set; }
        public DbSet<ApprovalLog> ApprovalLogs { get; set; }
        public DbSet<ApproverType> ApproverTypes { get; set; }
        #endregion

        #region Settings Configs
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {

            var entities = ChangeTracker.Entries().Where(x => x.Entity is Base && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = !string.IsNullOrEmpty(_httpContextAccessor?.HttpContext?.User?.Identity?.Name)
                ? _httpContextAccessor.HttpContext.User.Identity.Name
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Base)entity.Entity).createdAt = DateTime.Now;
                    ((Base)entity.Entity).createdBy = currentUsername;
                }
                else
                {
                    entity.Property("createdAt").IsModified = false;
                    entity.Property("createdBy").IsModified = false;
                    ((Base)entity.Entity).updatedAt = DateTime.Now;
                    ((Base)entity.Entity).updatedBy = currentUsername;
                }

                #region changelog
                //int sessionId = 0;
                //DateTime myDate1 = new DateTime(1970, 1, 9, 0, 0, 00);
                //DateTime myDate2 = DateTime.Now;
                //TimeSpan myDateResult;
                //myDateResult = myDate2 - myDate1;
                //double seconds = myDateResult.TotalSeconds;
                //sessionId = Convert.ToInt32(seconds);

                //string entityName = entity.Entity.GetType().Name;
                //string entityState = entity.State.ToString();
                //if (entityName != "UserLogHistory")
                //{

                //    var builder = new ConfigurationBuilder()
                //                    .SetBasePath(Directory.GetCurrentDirectory())
                //                    .AddJsonFile("appsettings.json");

                //    var configuration = builder.Build();

                //    using (var db = new SqlConnection(configuration.GetConnectionString("AlphaConnection")))
                //    {
                //        db.Open();

                //        var entityMember = entity.Members;
                //        var value = entity.Members.Count();
                //        var entityinfo = entity.Entity.GetType();
                //        var entityVal = entity.Entity;
                //        string customAttributeName = string.Empty;
                //        var fieldName = entityinfo.GetProperties();
                //        for (int i = 0; i < fieldName.Count(); i++)
                //        {
                //            var columnName = fieldName[i].Name;
                //            string colType = fieldName[i].PropertyType.ToString();
                //            var custmAttribute = fieldName[i].GetCustomAttributesData();
                //            if (custmAttribute.Count() >= 1)
                //                customAttributeName = custmAttribute.FirstOrDefault().AttributeType.Name;

                //            if (colType.Contains("AlphaManagement") || customAttributeName == "NotMappedAttribute")
                //            {

                //            }
                //            else
                //            {

                //                var valueName = entity?.Property(columnName)?.CurrentValue?.ToString();
                //                valueName = valueName?.Replace("'", "''");
                //                string Tmp1 = $"INSERT INTO DbChangeHistories (entityName,fieldName,fieldValue,entityState,sessionId,createdBy,createdAt) VALUES('{entityName}','{columnName}','{valueName}','{entityState}','{sessionId}','{currentUsername}','{DateTime.Now}');";
                //                SqlCommand cmd1 = new SqlCommand(Tmp1, db);
                //                cmd1.ExecuteScalar();
                //            }

                //        }
                //        db.Close();
                //    }

                //}

                #endregion

            }
        }
        #endregion

        #region Ogranogram
        public DbSet<OrganizationType> OrganizationTypes { get; set; }
        public DbSet<OrganoOrganization> OrganoOrganizations { get; set; }
        public DbSet<Post> Posts { get; set; }
        #endregion

        #region SP List
        public DbQuery<SuggestedBranched> suggestedBrancheds { get; set; }
        public DbQuery<AssignmentPostingModel> assignmentPostingModels { get; set; }
        public DbQuery<EmployeeInfosViewModelFor_SP> employeeInfos_Sp { get; set; }
        public DbQuery<GetCheckedEmployeeList_SP> SP_CheckedEmployeeInfoList { get; set; }
        public DbQuery<GetEmployeeListWithTrainingSkill> getEmployeeListWithTrainingSkills { get; set; }
        public DbQuery<SearchEmployee_Sp> searchEmployee_Sps { get; set; }
        public DbQuery<RankWiseVacancyViewModel> rankWiseVacancyViewModels { get; set; }
        public DbQuery<RankWiseOverDueModel> rankWiseOverDueModels{ get; set; }
        public DbQuery<UnitWiseOverduePostingModel> unitWiseOverduePostingModels{ get; set; }
        public DbQuery<UnitWiseEmployeeInfosViewModelFor_SP> unitWiseEmployeeInfos_Sp { get; set; }
        public DbQuery<AssignmentViewModels> assignmentViewModels { get; set; }
        public DbQuery<UnitRankWiseEmployeeViewModel> unitRankWiseEmployeeViewModels { get; set; }
        public DbQuery<InternalUnitRankWiseEmployeeViewModel> internalUnitRankWiseEmployeeViewModels { get; set; }
        public DbQuery<OfficerImageModel> OfficerImageModels { get; set; }
        public DbQuery<PMCorrectionDataModel> pMCorrectionDataModels { get; set; }
        public DbQuery<EmployeeGradationSPModel> employeeGradationSPs { get; set; }
        public DbQuery<EmployeePreviousPostingPlaceSPModel> employeePreviousPostingPlaceSPs { get; set; }
        public DbQuery<EmployeeGradationInfoSPModel> employeeGradationInfo { get; set; }
        public DbQuery<UpdateEmployeeGredationModel> updateEmployeeGredations { get; set; }
        #endregion

    }
}
