using PoliceOfficerManagement.Data.Entity;

namespace PoliceOfficerManagement.Areas.MasterData.Models
{
    public class MasterDataModel
    {
        public IEnumerable<RangeMetro> rangeMetro { get; set; }
        public IEnumerable<DivisionDistrict> districts { get; set; }
        public IEnumerable<ZoneCircle> zoneCircle { get; set; }
        public IEnumerable<PoliceThana> thanaInfo { get; set; }
    }
}
