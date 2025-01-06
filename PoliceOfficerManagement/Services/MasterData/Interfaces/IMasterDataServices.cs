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
        Task<int> SaveRangeMetro(RangeMetro model);
        Task<int> InActiveRangeMetroById(int Id);
        Task<IEnumerable<ZoneCircle>> GetZoneCircleByDivisionDistrictId(int divisionDistrictId);
        Task<IEnumerable<DivisionDistrict>> GetDivisionDistrictByRangeId(int rangeId);
        Task<IEnumerable<PoliceThana>> GetThanasByRangeId(int zoneId);
        Task<IEnumerable<InstitutionInfo>> GetAllInstitutionInfoForTraning();
        Task<IEnumerable<InstitutionInfo>> GetInstitutionInfoTraning();
        Task<IEnumerable<DivisionDistrict>> GetAllDivisionDistrict();
        Task<int> SaveDivisionDistrict(DivisionDistrict model);
        Task<int> InActiveDivisionDistrictById(int Id);
        Task<IEnumerable<ZoneCircle>> GetAllZoneCircle();
        Task<int> SaveZoneCircle(ZoneCircle model);
        Task<int> InActiveZoneCircleById(int Id);
        Task<int> InActivePoliceThanaById(int Id);
        Task<int> SavePoliceThana(PoliceThana model);
        Task<IEnumerable<PoliceThana>> GetAllPoliceThana();
        Task<IEnumerable<RangeMetro>> GetAllRangeMetros2();
        Task<IEnumerable<DivisionDistrict>> GetAllDivisionDistrict2();
        Task<IEnumerable<PoliceThana>> GetAllPoliceThana2();
    }
}
