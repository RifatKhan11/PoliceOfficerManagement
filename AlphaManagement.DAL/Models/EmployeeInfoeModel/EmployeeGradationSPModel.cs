using System.ComponentModel;

namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class EmployeeGradationSPModel
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
        public string commentDate { get; set; }
        public string LPRDate { get; set; }
        public string rankName { get; set; }
        public string joiningRank { get; set; }
        public string bpNo { get; set; }
        public string experienceName { get; set; }
        public string expReqDate { get; set; }
        public string educationQualification { get; set; }
        public string educationQualificationOld { get; set; }
        public string homeDistrict { get; set; }
        public string imageUrl { get; set; }
        public string batchName { get; set; }
        public string batchNameBN { get; set; }
        public int? IsPRL { get; set; }
        public string bcsPosition { get; set; }
        public string SpouseAddress { get; set; }
        public string gender { get; set; }
        public string regularJoin { get; set; }
        public string currentPostiongPlace { get; set; }
        public string joiningDatePresentWorkstation { get; set; }
        //Militery Officer Info batchName,G.bcsPosition
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
    }

    public class EmployeeGradationInfoSPModel
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
        public string commentDate { get; set; }
        public string LPRDate { get; set; }
        public string rankName { get; set; }
        public string joiningRank { get; set; }
        public string bpNo { get; set; }
        public string experienceName { get; set; }
        public string expReqDate { get; set; }
        public string educationQualification { get; set; }
        public string educationQualificationOld { get; set; }
        public string homeDistrict { get; set; }
        public string imageUrl { get; set; }
        public string batchName { get; set; }
        public string batchNameBN { get; set; }
        public int? IsPRL { get; set; }
        public string bcsPosition { get; set; }
        public string SpouseAddress { get; set; }
        public string gender { get; set; }
        public string regularJoin { get; set; }
        public string currentPostiongPlace { get; set; }

        //Militery Officer Info batchName,G.bcsPosition
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
        public string joiningDatePresentWorkstation { get; set; }
    }

    public class EmployeePreviousPostingPlaceSPModel
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
        public string commentDate { get; set; }
        public string LPRDate { get; set; }
        public string rankName { get; set; }
        public string joiningRank { get; set; }
        public string bpNo { get; set; }
        public string experienceName { get; set; }
        public string expReqDate { get; set; }
        public string educationQualification { get; set; }
        public string educationQualificationOld { get; set; }
        public string homeDistrict { get; set; }
        public string imageUrl { get; set; }
        public string batchName { get; set; }
        public string batchNameBN { get; set; }
        public int? IsPRL { get; set; }
        public string bcsPosition { get; set; }
        public string SpouseAddress { get; set; }
        public string gender { get; set; }
        public string regularJoin { get; set; }
        public string currentPostiongPlace { get; set; }
        public string joiningDatePresentWorkstation { get; set; }
        //Militery Officer Info batchName,G.bcsPosition
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
        public string previousPostingplaces { get; set; }
    }

    public class EmployeeGradationVMModel
    {
        [Description("ক্রমিক নং")]
        public int? slNo { get; set; }
        [Description("কর্মকর্তার নাম,জন্ম তারিখ, জন্ম জেলা ও শিক্ষাগত যোগ্যতা")]
        public string nameBangla { get; set; }
        [Description("অভিজ্ঞতা (পদের নাম) নিয়োগের তারিখসহ")]
        public string experienceName { get; set; }
        [Description("পুলিশ ক্যাডারের প্রারম্ভিক পদে কমিশনের সুপারিশ অনুযায়ী নিয়মিত নিয়োগ (সরাসরি / পদোন্নতি)")]
        public string joiningDateRank { get; set; }
        [Description("সিনিয়র স্কেলে নিয়মিত পদোন্নতির তারিখ")]
        public string firstPromotionDate { get; set; }
        [Description("সামরিক কর্মকর্তাদের প্রাসঙ্গিক তথ্যঃ - ক) কমিশন প্রাপ্তির তারিখ খ) অবসর গ্রহণের তারিখ")]
        public string commissionReceiptDate { get; set; }
        [Description("বর্তমান পদে নিয়মিত নিয়োগের তারিখ")]
        public string currentrankName { get; set; }
        [Description("মন্তব্য")]
        public string comments { get; set; }

        [Description("ক্যাডারে প্রথমে এ্যাডহক")]
        public string Adhoc { get; set; }

    }
}
