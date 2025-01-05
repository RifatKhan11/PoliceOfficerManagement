using PoliceOfficerManagement.Data.Entity;

namespace PoliceOfficerManagement.Services.MasterData.Interfaces
{
    public interface IMasterDataServices
    {
        Task<int> SaveInstitutionInfo(InstitutionInfo model);
        Task<IEnumerable<InstitutionInfo>> GetInstitutionInfo();
        Task<int> InActiveInstitutionInfoById(int Id);
        Task<int> SaveRank(Rank model);
        Task<IEnumerable<Rank>> GetRank();
        Task<int> InActiveRankById(int Id);
        Task<IEnumerable<Division>> GetAllDivision();
        Task<IEnumerable<District>> GetDistrictsByDivisionId(int divisionId);
        Task<IEnumerable<Thana>> GetThanasByDistrictId(int districtId);
        Task<IEnumerable<UnionWard>> GetUnionWardsByThanaId(int thanaId);
        Task<IEnumerable<Village>> GetUnionWardsByUnionWardId(int unionWardId);
        Task<IEnumerable<InstitutionInfo>> GetAllInstitutionInfo();
        Task<IEnumerable<RangeMetro>> GetAllRangeMetros();
        Task<IEnumerable<ZoneCircle>> GetZoneCircleByDivisionDistrictId(int divisionDistrictId);
        Task<IEnumerable<DivisionDistrict>> GetDivisionDistrictByRangeId(int rangeId);
        Task<IEnumerable<PoliceThana>> GetThanasByRangeId(int zoneId);
        Task<IEnumerable<InstitutionInfo>> GetAllInstitutionInfoForTraning();
    }
}
