namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class GradationViewModel
    {
        public int? Id { get; set; }
        public int? employeeId { get; set; }
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public string nameBangla { get; set; }
        public string dateOfBirth { get; set; }
        public string joiningDateGovtService { get; set; }
        public string firstPromotionDate { get; set; }
        public string lastPromotionDate { get; set; }
        public string joiningDatePresentWorkstation { get; set; }
        public string commentDate { get; set; }
        public int? rankId { get; set; }
        public int? branchId { get; set; }
        public int? sectionId { get; set; }
        public int? batchId { get; set; }
        public int? bcsPosition { get; set; }
        public int? organizationId { get; set; }
        public int? degreeId { get; set; }
        public string rankName { get; set; }
        public string joiningRank { get; set; }
        public string bpNo { get; set; }
        public string experienceName { get; set; }
        public string expReqDate { get; set; }
        public string educationQualification { get; set; }
        public string homeDistrict { get; set; }
        public string gender { get; set; }
        public string imageUrl { get; set; }
        public int? status { get; set; }
        public string SpouseAddress { get; set; }
        public string regularJoin { get; set; }
        //Militery Officer Info
        public string commissionReceiptDate { get; set; }
        public string commissionLPRDate { get; set; }
        public string civilReqDate { get; set; }
        public string civilRank { get; set; }
        public string comments { get; set; }
        public string firstAdhoc { get; set; }
        public string secondAdhoc { get; set; }
        public string thirdAdhoc { get; set; }
        public string forthAdhoc { get; set; }
        public string gradationSerial { get; set; }
        public string userName { get; set; }

        //Spouse Address
        public int? SpDivisionId { get; set; }
        public int? SpDistrictId { get; set; }
        public int? SpThanaId { get; set; }
    }
}
