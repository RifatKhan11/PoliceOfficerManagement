using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Models
{
    public class EmployeeInfosViewModelFor_SP
    {
        public int? Id { get; set; }
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public string rankName { get; set; }
        public string sectionName { get; set; }
        public string branchUnitName { get; set; }
        public string batchName { get; set; }
        public string imageUrl { get; set; }
        public int? statusMark { get; set; }
        public int? isApproved { get; set; }
        //public int? gradationSerial { get; set; }
    }

    public class UnitWiseEmployeeInfosViewModelFor_SP
    {
        public int? Id { get; set; }
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public string rankName { get; set; }
        public string sectionName { get; set; }
        public string branchUnitName { get; set; }
        public string batchName { get; set; }
        public string imageUrl { get; set; }
        public int? statusMark { get; set; }
        public int? isApproved { get; set; }
        public int? unitId { get; set; }

        //SP_Employee_Percent_Prograss,SP_GetUnitWiseProtfolioListWithStatus
    }

    public class GetCheckedEmployeeList_SP
    {
        public int? Id { get; set; }
        public string nameEnglish { get; set; }
        public string employeeCode { get; set; }
        public string rankName { get; set; }
        public string branchUnitName { get; set; }
        public string batchName { get; set; }
        public string url { get; set; }
    }

    public class GetEmployeeListWithTrainingSkill
    {
        public int? Id { get; set; }
        public string nameEnglish { get; set; }
        public string employeeCode { get; set; }
        public string rankName { get; set; }
        public string branchUnitName { get; set; }
        public string batchName { get; set; }
        public string url { get; set; }
    }
}
