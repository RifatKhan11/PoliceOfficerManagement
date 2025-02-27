using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.AddressData.Models
{
    public class AddressInformationViewModel
    {
        public int AddressInformationId { get; set; }
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public int? spouseId { get; set; }
        public Spouse spouse { get; set; }

        public int? countryId { get; set; }
        public Country country { get; set; }
        public int? divisionId { get; set; }
        public Division division { get; set; }
        public int? districtId { get; set; }
        public District district { get; set; }
        public int? thanaId { get; set; }
        public Thana thana { get; set; }
        public int? unionWardId { get; set; }
        public UnionWard unionWard { get; set; }
        public int? villageId { get; set; }
        public Village village { get; set; }

        public string union { get; set; }

        public string postOffice { get; set; }

        public string postCode { get; set; }

        public string blockSector { get; set; }

        public string houseVillage { get; set; }

        public string roadNumber { get; set; }

        public string addressDetails { get; set; }

        public string oneLineAddress { get; set; }
        //Type: Permamnent or Present
        public string type { get; set; }
        public AddressInformationLn fLang { get; set; }
        public AddressInformation addressInformation { get; set; }
        public IEnumerable<AddressInformation> addressInformationList { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfoList { get; set; }
        public IEnumerable<Spouse> spouseList { get; set; }
        public IEnumerable<Country> countryList { get; set; }
        public IEnumerable<Division> divisionList { get; set; }
        public IEnumerable<District> districtList { get; set; }
        public IEnumerable<Thana> thanaList { get; set; }
        public IEnumerable<UnionWard> unionWardList { get; set; }
        public IEnumerable<Village> villageList { get; set; }
    }
}
