using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.Employee.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class PromotionLogViewModel
    {
        public int PromotionLogId { get; set; }
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
        public int? designationNewId { get; set; }
        public Designation designationNew { get; set; }
        public int? designationOldId { get; set; }
        public Designation designationOld { get; set; }
        public DateTime date { get; set; }
        public int? payScaleId { get; set; }
        public SalaryGrade payScale { get; set; }
        public string goNumber { get; set; }
        public DateTime? goDate { get; set; }
        public string remark { get; set; }
        public PromotionLogLn fLang { get; set; }
        public PromotionLog promotionLog { get; set; }
        public IEnumerable<PromotionLog> promotionLogs { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public IEnumerable<Designation> newDesignations { get; set; }
        public IEnumerable<Designation> oldDesignations { get; set; }
        public IEnumerable<SalaryGrade> payScales { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
    }
}
