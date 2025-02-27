using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
   public class PostingPriorityLevel:Base
    {
        public int? priorityLevelTypeId { get; set; }
        public PriorityLevelType priorityLevelType { get; set; }
        public int? status { get; set; } //1=Ignore 2 = Required 3 = not  fixed 
        public string prorityLevel { get; set; }
        public int? sortOrder { get; set; }
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public int? rankId { get; set; }
        public Rank rank { get; set; }
        public int? specialBranchUnitId { get; set; }
        public SpecialBranchUnit specialBranchUnit { get; set; }
        public int? specialSkillTypeId { get; set; }
        public SpecialSkillType specialSkillType { get; set; }
        public int? districtId { get; set; }
        public District district { get; set; }
        public string ruleDescription { get; set; }
        public string remarks { get; set; }
    }
}
