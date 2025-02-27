using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class EmployeeDetailsViewModel
    {
        public int? empId { get; set; }
        public string empName { get; set; }
        public string empNameBn { get; set; }
        public string bpNo { get; set; }
        public string fatherName { get; set; }
        public string spouseName { get; set; }
        public string unitName { get; set; }
        public string sectionName { get; set; }
        public string rankName { get; set; }
        public string bcsBatch { get; set; }
        public int? bcsPosition { get; set; }
        public string presentAddress { get; set; }
        public string permanentAddress { get; set; }
        public string spouseAddress { get; set; }
        public string educationQualification { get; set; }
        public string imageUrl { get; set; }
        public DateTime? joiningDate { get; set; }
        public DateTime? joiningDateOfPresentUnit { get; set; }
        public DateTime? LPRDate { get; set; }
        public DateTime? lastPromotionDate { get; set; }
        public int? rankId { get; set; }
        public int? batchId { get; set; }
    }
}
