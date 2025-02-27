using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.ApprovalMatrix;
using AlphaManagement.DAL.Entity.EmployeeInfoHistories;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.InternalPosting;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.Web.Areas.Employee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.EmployeeService.interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<UnitWiseOverduePostingModel>> UnitWiseOverduePosting();
        Task<IEnumerable<RankWiseOverDueModel>> RankWiseOverDueCount();
        Task<IEnumerable<RankWiseVacancyViewModel>> RankWiseVacancyCount();
        void UpdateEmployeeInfoStatusById(int empId, int status);
        EmployeeInfo GetBasicEmployeeInfoById(int id);
        Task<IEnumerable<EmployeeInfosViewModelFor_SP>> GetEmployeeInfoListForSp(string userName, int unitId, int rankId, int batchId, int statusId, string periodType);
        Task<IEnumerable<UnitWiseEmployeeInfosViewModelFor_SP>> GetUnitWiseEmployeeInfoListForSp(string userName, int unitId, int rankId, int batchId, int divisionId);
        Task<IEnumerable<UnitWiseEmployeeInfosViewModelFor_SP>> GetEmployeePercentPrograssList(int? statusId);
        Task<IEnumerable<GetCheckedEmployeeList_SP>> GetCheckedEmployeeListBySp(int unitId, int rankId, int batchId);
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoList(int statusId);
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfosByType(int typeId);
        Task<IEnumerable<PostingReportView>> PendingAssignmentMasters(string userId);
        Task<IEnumerable<PostingReportView>> ApprovedAssignmentMasters(string userId);
        Task<IEnumerable<AssignmentAnulipiPreview>> AssignmentMastersRopoForPreview(int id);
        Task<IEnumerable<Rank>> GetRankWiseCount();
        Task<IEnumerable<EmployeeInfo>> GetEmployeeForChartJs();
        Task<IEnumerable<PromotionLog>> GetPromotionLogs(int empId);
        Task<IEnumerable<EmployeeListVM>> GetEmployeeListById();
        Task<IEnumerable<EmployeeListVM>> GetEmployeeListForAlphaPersonalProfile();
        Task<IEnumerable<GetEmployeeListWithTrainingSkill>> GetEmployeeListWithTrainingSkills(int unitId, int rankId, int batchId);
        Task<IEnumerable<GetEmployeeListWithTrainingSkill>> GetAllEmployeeList(int unitId, int rankId, int batchId,string userName);
        Task<IEnumerable<EmployeeListVM>> GetPRLEmployeeListForAlphaProfile(int? rankId, int? unitId, int? bcsBatchId);
        Task<IEnumerable<Assignment>> AssignmentPostedByMasterId(int assignid);
        Task<IEnumerable<AssignmentVM>> AssignmentPostedByMasteId(int assignid);
        Task<IEnumerable<AssignmentViewModels>> GetAssignmentListbyMasterId(int masterid);
        Task<IEnumerable<AssignmentAnulipi>> AssignmentMastersRopo(int id);
        Task<IEnumerable<Assignment>> AssignmentMastersEntry(int id);
        Task<IEnumerable<Assignment>> AssignmentMastersReport(int assignmentId);
        Task<IEnumerable<PostingReportView>> GetApprovalLogFullByMasterId(int masterId);
        IQueryable<EmployeeDetailsViewModel> GetOfficerSearchInformation(int unitId, int rankId, int batchId, string bpNo);
        // Task<IEnumerable<Assignment>> AssignmentMastersPostedReport(string id, int assignid);
        Task<IEnumerable<PostingReportView>> AssignmentPostedMastersApprove(string userId);
        Task<IEnumerable<PostingReportView>> AssignmentPostedMasters(string userId);
        Task<IEnumerable<PostingReportView>> AssignmentPostedMastersAll();
        Task<IEnumerable<Assignment>> AssignmentMastersPosted();
        Task<IEnumerable<PostingReportView>> AssignmentMasters(string userId);
        Task<IEnumerable<PostingReportView>> AssignmentPostedMastersForApprove(string userId);
        Task<IEnumerable<PostingReportView>> AssignmentMastersAll();
        Task<IEnumerable<Assignment>> GetAssignments();
        Task<IEnumerable<Section>> GetSectionWiseUnit();
        Task<EmployeeInfo> EmployeeInfoDetails(int id);
        Task<IEnumerable<Disease>> GetDiseases();
        Task<IEnumerable<Vaccines>> GetVaccines();
        Task<IEnumerable<DisciplinaryAction>> GetDisciplinaryActions(int empId);
        Task<EmployeeInfo> GetEmployeeInfosByEmpId(int empId);
        Task<IEnumerable<EmployeeListVM>> GetEmployeeSearchByDistrictId(int districtId);
        Task<IEnumerable<EmployeeListVM>> GetEmployeeSearchByRankId(int rankId);
        Task<IEnumerable<EmployeeListVM>> GetEmployeeSearchByUnitId(int unitId);
        Task<IEnumerable<ForeignTravel>> GetForeignTravelsById(int id);
        Task<IEnumerable<ForeignTravel>> GetForeignTravelsForFATById(int id);
        Task<IEnumerable<SpecialBranchUnit>> GetSpecialBranchUnitParent();
        Task<IEnumerable<SpecialBranchUnit>> GetSpecialBranchUnitChild(int id);
        Task<int> SaveForeignTravel(ForeignTravel foreignTravel);
        Task<int> DeleteForeignTravel(int id);
        Task<EmployeeInfo> GetBankByEmpId(int bankId);
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfos();
        Task<IEnumerable<ACRInformation>> GetEmployeeACRInfo(int id);
        Task<EmployeeInfo> GetEmployeeProfileInfoById(int id);
        Task<Photograph> GetEmployeeSignatureByEmpId(int empId);
        Task<Photograph> GetPhotographByEmpId(int empId);
        Task<EmployeeInfoVM> GetPhotographByUserId(string userId);
        Task<int> SaveEmployeeInformation(EmployeeInfo employeeInfo);
        Task<int> SaveEmployeeGradationInformation(EmployeeGradation employeeInfo);
        Task<EmployeeInfo> GetEmployeeInfoById(string empCode);
        Task<Photograph> GetEmployeePhotographByEmpId(int empId);
        Task<int> SavePromotionInfo(PromotionLog promotionLog);
        Task<int> DeletePromotionInfoById(int id);
        Task<IEnumerable<PromotionLog>> GetPromotionInfoByEmpId(int empId);
        Task<int> SaveFamilyInformation(Spouse spouse);
        Task<int> DeleteFamilyInfoById(int id);
        Task<IEnumerable<Spouse>> GetSpouseInfoByEmpId(int empId);
        Task<int> DeletePresentAddressById(int id);
        Task<int> SaveAssignmentInformation(Assignment assignment);
        Task<IEnumerable<Assignment>> GetAssignmentInfoByEmpId(int empId);
        Task<int> DeleteAssignmentInfoById(int id);
        void DeleteAssignmentsAunilipiPreviewByMasterId(int id);
        Task<int> SaveEducationInformation(EducationalQualification educationalQualification);
        Task<IEnumerable<EducationalQualification>> GetEducationalQualificationInfoByEmpId(int empId);
        Task<int> DeleteEducationalQualificationInfoById(int id);
        Task<int> SaveTrainingInformation(TraningLog traningLog);
        Task<IEnumerable<TraningLog>> GetTraningLogInfoByEmpId(int empId);
        Task<IEnumerable<TraningLog>> GetTraningLogInfoByEmpIdType(int empId, string type);
        Task<int> DeleteTrainingInfoById(int id);
        Task<int> SaveAwardInformation(AwardEntry awardEntry);
        Task<IEnumerable<AwardEntry>> GetAwardInfoByEmpId(int empId);
        Task<int> DeleteAwardById(int id);
        Task<int> SaveDisciplinaryInformation(DisciplinaryAction disciplinaryAction);
        Task<IEnumerable<DisciplinaryAction>> GetDisciplinaryByEmpId(int empId);
        Task<IEnumerable<EmployeeInfo>> GetOverDueEmployeeInfoList();
        Task<IEnumerable<EmployeeInfo>> GetOverDueEmployeeInfoListFilter(int unit, int rank, int batch);
        Task<IEnumerable<EmployeeInfo>> GetOverDueEmployeeInfoList(int rank, int unit, int batch);
        Task<Disease> GetDiseasesInfoById(int id);
        Task<EmployeeAPIModel> GetEmployeeInfoByBP(string BP);
        Task<int> DeleteDisciplinaryById(int id);
        Task<Spouse> GetSpouseInfoByEmpIdRelId(int empId);
        Task<int> UpdateEmployeeInfo(int id);
        Task<int> UpdateEmployeeInfoAfterArticle47(int id);
        Task<int> UpdateAssignmentInfoForIGP(int id); Task<int?> AssignmentDueCount();
        void UpdateEmployeeInfoisAdminCheckStatusById(int empId, int status);
        void UpdateApprovalLogBymasterId(int masterId);
        Task<int> UpdateEmployeeDataFromGradationById(int gradationId, string userName);
        Task<IEnumerable<EmployeeInfo>> GetCheckedEmployeeInfoList();
        Task<IEnumerable<EmployeeInfo>> LoadCheckedUpdatedEmployeeInfoList(int rankId, int unitId, int batchId);
        Task<IEnumerable<ApprovalLog>> GetApprovalLogByMasterId(int masterId);
        //Task<string> CheckDistrictTypeBySecIdAndEmpId(int sectionid, int employeeid);
        Task<IEnumerable<CheckBranchViewModel>> CheckDistrictTypeBySecIdAndEmpId(int branchId, int employeeid);
        Task<IEnumerable<EmployeePercentWisePrograssModel>> GetPercentWisePrograss();

        Task<EmployeeGradation> GetEmployeeInfogradationById1(int empId);
        Task<EmployeeGradationSPModel> GetEmployeeInfogradationById(int employeeId);
        //Task<UpdateEmployeeGredationModel> UpdateEmployeeGredation(int gradationId, int status, string userName);
        Task<IEnumerable<EmployeeGradationSPModel>> GetAllInActiveEmployeeInfo(int rankId, int batchId, string fromDate, string toDate, string prlFromDate, string prlToDate, int? typeId, string userName, string bpNoSearch);
        #region Address
        Task<int> SaveAddressInformation(AddressInformation addressInformation);
        Task<IEnumerable<AddressInformation>> GetAddressInformationByEmpId(int empId);
        Task<int> DeleteAddressInfoByEmpIdType(int empId, string type);
        Task<AddressInformation> GetAddressByEmpIdType(int empId, string type);
        #endregion
        #region Medical
        Task<int> SaveMedicalInfo(MedicalInfo medicalInfo);
        Task<IEnumerable<MedicalInfo>> GetMedicalInfoByEmpIdType(int empId);
        Task<MedicalInfo> GetMedicalInfoByEmpIdMedId(int empId, int medId);
        Task<IEnumerable<MedicalInfoViewModel>> GetMedicalInfoByEmpId(int empId);
        Task<MedicalInfo> GetMedicalInfoByMedId(int medicalId);
        Task<int> DeleteMedicalInfoById(int id);
        Task<int> SaveMedicalDiseaseInfo(EmployeeMedicalDisease medicalInfo);
        Task<int> DeleteMedicalDiseaseInfoByMedicalId(int id);
        Task<int> SaveMedicalVaccineInfo(EmployeeMedicalVaccine medicalInfo);
        Task<IEnumerable<EmployeeMedicalDisease>> GetDiseasesInfoByMedicalId(int medId);
        #endregion
        #region Enlisted Assignment
        Task<int> SaveEnlistedAssignment(EnlistedAssignment assignment);
        Task<int> DeleteEnListedList(string refNum);
        Task<IEnumerable<EnlistedAssignment>> EnListedDetails(string userId);
        Task<IEnumerable<EnlistedAssignment>> FrezzDetails(string userId);
        Task<IEnumerable<EnlistedViewModel>> EnListedMasterDetails(string userId);
        Task<IEnumerable<EnlistedViewModel>> FrezzMasterDetails(string userId);
        Task<int> SaveEnlistedMasterAssignment(EnlistedAssignmentMaster assignment);
        Task<int> UpdateEnListedEmplyeeStatus(string refNo);
        Task<int> GetEnlistedMasterIdByRef(string refNo);
        Task<IEnumerable<Assignment>> AssignmentDetailsByMasterId(int id);
        Task<int> GetEnlistMasterId(string refNo);
        Task<Assignment> GetAssignmentByRefNoEmpId(string refNo, int empId);
        #endregion
        #region Section
        Task<Section> GetSectionInfoForEmployee(int id);
        Task<IEnumerable<Section>> GetPostingPlaceByBranchId(int id);
        #endregion
        #region Assignment
        Task<int> SaveAssignMaster(AssignmentMaster assignment);
        Task<int> SaveAssignDetails(Assignment assignment);
        Task<int> GetAssignmentsMasterIdByDeatilsId(int detailsId);
        Task<int> ReturnAssignmentMaster(int id);
        Task<IEnumerable<Assignment>> GetAssignmentsByMasterId(int id);
        Task<IEnumerable<PostingReportView>> AssignmentReturnList(string userId);
        Task<IEnumerable<PostingReportView>> AssignmentReturnListAll();
        Task<IEnumerable<Assignment>> AssignmentReturnDetailsByMasterId(int assignid);
        Task<IEnumerable<OfficerImageModel>> GetOfficerImages();
        #endregion
        #region Assignment Complete
        Task<IEnumerable<PostingReportView>> CompleteAssignmentMasters(string userId);
        #endregion
        #region Anulipi
        Task<AnulipiList> GetAnulipiFromListByName(string anulipi);
        #endregion
        Task<int> DeleteEnlistedAssignment(int id);
        Task<IEnumerable<ACRInformation>> GetACRInfoByEmpId(int id);
        Task<LPRDateModel> GetLPRDateByBirthDate(string birthDate);
        Task<EmployeeInfo> GetEmployeeInfoSingleById(string empCode);

        Task<int> GetStatusWiseEmployeeCount(int statusid);
        Task<List<UnitWiseEmployeeCountModel>> GetUnitWiseEmployeeCount();
        Task<RankWiseEmployeeCountModel> GetRankWiseEmployeeCount();
        Task<List<DivisionWiseEmployeeCountModel>> GetDivisionWiseEmployeeCount();
        Task<BatchWiseEmployeeCountModel> GetBatchWiseEmployeeCount();

        Task<int> GetTodaysEmployeesStatusWise(int statusid);
        Task<IEnumerable<Assignment>> AssignmentAll();
        Task<IEnumerable<GlobalSearchModel>> GetGlobalSearchInfo(string userName, string filter);

        Task<List<EmployeeReport>> GetEmployeeInfos(string queryString);
        Task<IEnumerable<EmployeeReport>> GetEmployeeInformationByQueryList(EmployeeReport model);
        Task<IEnumerable<EmployeeReport>> GetEmployeeInformationByQueryListForGradation(EmployeeReport model);
        Task<IEnumerable<EmployeeGradationSPModel>> GetEmployeeInformationForGradation(int rankId, int batchId, string fromDate, string toDate, string prlFromDate, string prlToDate, int? typeId, string userName, string bpNoSearch, string gender);

        Task<IEnumerable<EmployeeGradationSPModel>> GetEmployeeInformationForGradationNew(int rankId, int batchId, string fromDate, string toDate, string prlFromDate, string prlToDate, int? typeId, string userName, string bpNoSearch, string gender);
        Task<IEnumerable<EmployeeGradationInfoSPModel>> GetEmployeeGradationInformationNew(int rankId, int batchId, string fromDate, string toDate, string prlFromDate, string prlToDate, int? typeId, string userName, string bpNoSearch, string gender);
        Task<IEnumerable<EmployeePreviousPostingPlaceSPModel>> GetEmployeeInformationWithPreviousPosting(int rankId, int batchId, string fromDate, string toDate, string prlFromDate, string prlToDate, int? typeId, string userName, string bpNoSearch, string gender);
        Task<bool> UpdateEmployeeGradation(GradationViewModel model);
        Task<IEnumerable<EmployeeGradation>> GetEmployeeInformationForGradationUpdate(int status);
        Task<IEnumerable<EmployeeReturnReason>> GetEmployeeReturnReasonByEmpId(int empId);
        Task<EmployeeGradation> GetEmployeeInformationForGradation(int empId);

        Task<int> GetCheckedEmployeeCount(int statusid);
        Task<int> GetCheckedEmployeeCountByChecker(int statusid, string userName);
        Task<IEnumerable<EmployeeInfo>> GetCheckedUpdatedEmployeeInfoList();

        Task<RankWiseEmployeeCountModel> GetRankWiseEmployeeCountChecked();
        Task<IEnumerable<EmployeeReturnReason>> GetEmployeeReturnReasonEmployee(int empId);
        Task<IEnumerable<EmployeeInfo>> GetCheckedUpdatedEmployeeInfoListUpdate();
        Task<int> DeleteReturnResaonById(int id);
        Task<IEnumerable<GetPortfolioTransectionHistoryLog>> GetPortfolioTransectionHistoryLog(int empId);
        #region History 
        Task<int> SaveEmployeeEmployeeInfoHistory(EmployeeInfoHistory employeeInfo);
        Task<AddressInformation> GetAddressById(int Id);
        Task<int> SaveAddressInformationHistory(AddressInformationHistory addressInformation);
        Task<Assignment> GetAssignmentInfoById(int Id);
        Task<int> SaveAssignmentAssignmentHistory(AssignmentHistory assignment);
        Task<PromotionLog> GetPromotionInfoById(int Id);
        Task<int> SavePromotionLogHistory(PromotionLogHistory promotionLog);
        Task<EducationalQualification> GetEducationalQualificationInfoById(int Id);
        Task<int> SaveEducationalQualificationHistory(EducationalQualificationHistory educationalQualification);
        Task<TraningLog> GetTraningLogInfoById(int Id);
        Task<int> SaveTraningLogHistory(TraningLogHistory traningLog);
        Task<AwardEntry> GetAwardInfoById(int Id);
        Task<int> SaveAwardEntryHistory(AwardEntryHistory awardEntry);
        Task<int> SaveForeignTravelHistory(ForeignTravelHistory foreignTravel);
        Task<ForeignTravel> GetForeignTravelById(int Id);
        Task<int> SaveSpouseHistory(SpouseHistory spouse);
        Task<Spouse> GeSpouseById(int Id);


        Task<EmployeeInfoHistory> GetEmployeeInfoHistoryById(string empCode);
        Task<IEnumerable<SpouseHistory>> GetSpouseHistoryByEmpId(int empId);
        Task<IEnumerable<AssignmentHistory>> GetAssignmentHistoryByEmpId(int empId);
        Task<IEnumerable<EducationalQualificationHistory>> GetEducationalQualificationHistoryByEmpId(int empId);
        Task<IEnumerable<AwardEntryHistory>> GetAwardEntryHistoryByEmpId(int empId);
        Task<IEnumerable<PromotionLogHistory>> GetPromotionLogHistoryByEmpId(int empId);
        Task<IEnumerable<TraningLogHistory>> GetTraningLogHistoryByEmpId(int empId);
        Task<IEnumerable<AddressInformationHistory>> GetAddressInformationHistoryByEmpId(int empId);
        Task<IEnumerable<ForeignTravelHistory>> GetForeignTravelHistoryById(int id);
        #endregion
        Task<bool> SaveEmployeeTransectionHistoryLog(int empId, int statusId, string userId, string actionType, string remarks);
        Task<bool> SaveAlphaTransectionHistoryLog(int statusId, string userId, int actionId, string actionType, string remarks);
        Task<IEnumerable<SpecialBranchUnit>> GetEmployeePermanentAddUnit(int empId);
        Task<SpecialBranchUnit> GetEmployeePresentUnit(int empId);
        Task<IEnumerable<SpecialBranchUnit>> GetEmployeePreviousUnit(int empId);
        Task<IEnumerable<SpecialBranchUnit>> GetEmployeeSpouseUnit(int empId);
        Task<IEnumerable<PostingReportView>> AssignmentMasterLocked(int status);
        Task<IEnumerable<AssignmentDetailsModal>> AssignmentDetailsModalByMasterId(int assignid);
        Task<IEnumerable<Assignment>> GetAssignmentsPriviousLockByMasterId(int id);
        Task<int> DeleteAssignmentsPriviousLockByMasterId(int id);
        Task<IEnumerable<BadgeAndActivityModel>> GetBadgeAndActivityModelList();
        Task<IEnumerable<AssignmentDetailsModal>> LockedOfficersByMasterId(int masterId, int rankId);
        Task<IEnumerable<CancelAssignment>> GetAssignmentsRevisedByMasterId(int id);
        Task<PIMSDataModel> GetPIMSDataModel(string bp);
        Task<IEnumerable<PostingReportView>> AllLockedAssignmentMaster();
        Task<int?> AssignmentMastersCount(string userId);
        Task<int?> AssignmentPostedMastersApproveCount(string userId);
        Task<int?> AssignmentReturnListCount(string userId);
        Task<int?> AssignmentPostedMastersForApproveCount();
        Task<int?> AssignmentMasterLockedCount(int status);
        Task<int> ReturnInternalAssignmentMaster(int id);
        Task<int> GetTotalPHQEmployee();
        Task<IEnumerable<PostingReportView>> InternalAssignmentMasters(string userId);
        void UpdateInternalApprovalLogBymasterId(int masterId);
        Task<Photograph> GetEmployeeSignatureByBp(string bp);
        Task<int?> InternalAssignmentMastersCount(string userId);
        Task<IEnumerable<AssignmentViewModels>> GetInternalAssignmentListbyMasterId(int masterid);
        Task<IEnumerable<InternalAssignment>> InternalAssignmentPostedByMasterId(int assignid);
        Task<int> GetinternalAssignmentMasterIdByEnlishmentId(int id);
        Task<int> UpdateAssignmentInfoForIGPUndo(int id);
        Task<IEnumerable<EnlistedAssignment>> GetEnlistedListByRef(string refNo);
        Task<IEnumerable<PostingPriorityLevel>> GetPostingPriorityLevel();
        Task<int> SavePostingPriorityLevel(PostingPriorityLevel level);
        Task<PostingPriorityLevel> GetPostingPriorityLevelById(int id);
        Task<int> DeletePostingPriorityLevelById(int id);
        Task<EmployeeBasicInfoForPosting> GetEmployeeBasicInfoForPostingByempId(int Id);
        Task<IEnumerable<EmployeeReportInfo>> GetEmployeeReportInfoByEmpIdandType(int empId, string type);
        Task<int> UpdateTypeEnlistedAssignment(int empId, string refNo);
        Task<int> DeletePriorityLevelTypebyId(int id);
        Task<int> DeleteSpecialSkillById(int id);
        Task<int> DeleteMedicalMainCategoryById(int id);
        Task<int> DeletemedicalSubCategoryById(int id);
        Task<IEnumerable<MedicalSubCategory>> getMedicalSubCategoty();
        Task<IEnumerable<EmployeeMadicalInfo>> GetEmployeeMadicalInfoByEmpIde(int empId);
        Task<IEnumerable<MedicalSubCategory>> GetMedicalSubCatbyMainId(int id);
        Task<IEnumerable<GetCheckedEmployeeList_SP>> GetDiciplinaryEmployeeListBySp(int unitId, int rankId, int batchId);
        Task<IEnumerable<DisciplinaryAction>> GetDiciplinaryActionByEmpId(int empId);
        Task<IEnumerable<EmployeeInfo>> GetEmployeeReportInfoByType(string type, int branchId, int rankId, int bcs);
        Task<IEnumerable<GetCheckedEmployeeList_SP>> GetMedicalInfoEmployeeListBySp(int unitId, int rankId, int batchId);
        Task<IEnumerable<EmployeeMadicalInfo>> GetMedicalInfosByEmpId(int empId);
        Task<string> GetUserRole(string userId);
        Task<List<EmployeeSearchReport>> GetEmployeeInfosBySearch(string queryString);
        #region Employee Search By Bcs
        Task<IEnumerable<EmpolyeeDetailsByBCS>> GetEmployeeInfoByBCSBatch(int bcsBatchId);
        Task<IEnumerable<PMCorrectionDataModel>> GetCorrectionEmployeeInfoByBCSBatch(int batchId);
        #endregion
    }
}
