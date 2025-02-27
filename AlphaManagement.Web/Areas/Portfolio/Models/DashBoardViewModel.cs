using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Portfolio.Models
{
    public class DashBoardViewModel
    {
        public EmployeeInfo employeeInfo { get; set; }
        public ApplicationUser user { get; set; }
        public IList<string> userRole { get; set; }
        public IEnumerable<Country> countries { get; set; }
        public IEnumerable<ForeignTravel> foreignTravels { get; set; }
        public IEnumerable<Assignment> assignments { get; set; }
        public Assignment assignment { get; set; }
        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
        public IEnumerable<Rank> rank { get; set; }
        public IEnumerable<BCSBatch> batches { get; set; }

        #region Update Assignment For Article 47
        public int assignmentId { get; set; }
        public int type { get; set; }
        public int Schedule { get; set; }
        public DateTime joiningDate { get; set; }
        public DateTime releaseDate { get; set; }
        public string memorandomNo { get; set; }
        public string reasonofTransfer { get; set; }
        public int? supervisorId { get; set; }
        public string supervisorPhone { get; set; }
        public string supervisorFax { get; set; }
        public string supervisorEMail { get; set; }
        public int specialBranchUnitId { get; set; }
        public int ranksId { get; set; }
        public int subBranchId { get; set; }
        public int secId { get; set; }
        #endregion
    }    
}
