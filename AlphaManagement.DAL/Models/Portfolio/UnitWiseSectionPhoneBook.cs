using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models.Portfolio
{
    public class UnitWiseSectionPhoneBook
    {
        public string unitName { get; set; }
        public SpecialBranchUnit mainBranchUnit { get; set; }

        public string subUnitName { get; set; }
        public SpecialBranchUnit subBranchUnit { get; set; }

        public string sectionName { get; set; }
        public Section section { get; set; }

        public string departmentName { get; set; }
        public Department department { get; set; }

        public IEnumerable<BDPolicePhoneBook> phoneBooks { get; set; }
    }
}
