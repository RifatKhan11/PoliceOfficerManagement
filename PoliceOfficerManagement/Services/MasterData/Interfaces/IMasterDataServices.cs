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
        Task<int> SaveCountry(Country model);
        Task<IEnumerable<Country>> GetCountry();
        Task<int> InActiveCountryById(int Id);
        Task<int> SaveVillage(Village model);
        Task<IEnumerable<Village>> GetVillage();
        Task<int> InActiveVillageById(int Id);
        Task<int> SaveUnionWard(UnionWard model);
        Task<IEnumerable<UnionWard>> GetUnionWard();
        Task<int> InActiveUnionWardById(int Id);
        Task<int> SaveThana(Thana model);
        Task<IEnumerable<Thana>> GetThana();
        Task<int> InActiveThanaById(int Id);
        Task<int> SaveDistrict(District model);
        Task<IEnumerable<District>> GetDistrict();
        Task<int> InActiveDistrictById(int Id);
        Task<int> SaveDivision(Division model);
        Task<IEnumerable<Division>> GetDivision();
        Task<int> InActiveDivisionById(int Id);
        Task<IEnumerable<District>> GetDistrict2();
        Task<IEnumerable<UnionWard>> GetUnionWard2();
        Task<IEnumerable<Thana>> GetThana2();

    }
}
