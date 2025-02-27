using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class DepartmentViewModel
    {
        public int DepartmentId { get; set; }
        public string deptCode { get; set; }
        public string deptName { get; set; }
        public string deptNameBn { get; set; }
        public string shortName { get; set; }
        public DateTime? startDate { get; set; }
        public DepartmentLn fLang { get; set; }
        public Department department { get; set; }
        public IEnumerable<Department> departments { get; set; }

        //Offence     
        //Natural Punishment
        public NaturalPunishment naturalPunishment { get; set; }
        public Offense offense { get; set; }
        public IEnumerable<Offense> offenses { get; set; }
        public IEnumerable<NaturalPunishment> naturalPunishments { get; set; }
        public int offenseId { get; set; }
        public int naturalPunishmentId { get; set; }
        public string name { get; set; }    
        public string description { get; set; }
        public int? shortOrder { get; set; }     
      
    }
}
