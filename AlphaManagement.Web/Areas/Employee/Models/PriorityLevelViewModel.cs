using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class PriorityLevelViewModel
    {

        public int? Id { get; set; }
        public int? priorityLevelTypeId { get; set; }
        public int? status { get; set; } //1=Ignore 2 = Required 3 = not  fixed 
        public string prorityLevel { get; set; }
        public int prorityLevelId { get; set; }
        public int? sortOrder { get; set; }
        public int? employeeInfoId { get; set; }
        public int? rankId { get; set; }
        public int? specialBranchUnitId { get; set; }
        public int? specialSkillTypeId { get; set; }
        public int? districtId { get; set; }
        public string ruleDescription { get; set; }
        public string description { get; set; }
        public int? medicalMainCategoryId { get; set; }
        public string remarks { get; set; }
        public string name { get; set; }
        public string nameBn { get; set; }


        public IEnumerable<SpecialSkillType> specialSkillTypes { get; set; }
        public IEnumerable<Rank> ranks { get; set; }
        public IEnumerable<District> districts { get; set; }
        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
        public IEnumerable<PriorityLevelType> priorityLevelTypes { get; set; }
        public IEnumerable<PostingPriorityLevel> postingPriorityLevels { get; set; }
        public IEnumerable<MedicalMainCategory> medicalMainCategories { get; set; }
        public IEnumerable<MedicalSubCategory> medicalSubCategories { get; set; }
    }
}
