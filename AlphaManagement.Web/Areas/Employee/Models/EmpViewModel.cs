using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfoHistories;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class EmpViewModel
    {
        public IEnumerable<ApplicationRole> applicationRoles { get; set; }
        public EmployeeInfo Employee { get; set; }
        public Photograph EmpPhotograph { get; set; }
        public IEnumerable<Rank> ranks { get; set; }
        public IEnumerable<BCSBatch> bCSBatches { get; set; }
        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
        public IEnumerable<Organization> organizations { get; set; }
        public IEnumerable<EmployeeGradationSPModel> employeeGradationSPs { get; set; }
        public IEnumerable<EmployeePreviousPostingPlaceSPModel> employeePreviousPostingPlaceSPs { get; set; }
        public IEnumerable<EmployeeGradation> employeeGradations { get; set; }
        public IEnumerable<Degree> degreeList { get; set; }
        public IEnumerable<Spouse> spouseList { get; set; }
        public IEnumerable<District> districtList { get; set; }
    }
}
