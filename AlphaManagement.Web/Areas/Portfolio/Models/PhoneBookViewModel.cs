using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models.Portfolio;
using AlphaManagement.Web.Models.JsonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Portfolio.Models
{
    public class PhoneBookViewModel
    {
        public IList<UnitWiseSectionPhoneBook> unitWisePhoneBooks { get; set; }
        public IList<JsonRank> jsonRanks { get; set; }
        public IList<JsonUnit> jsonUnits { get; set; }
        public IList<JsonContact> jsonContacts { get; set; }
    }
}
