using PoliceOfficerManagement.Data.Entity;

namespace PoliceOfficerManagement.Services.MasterData.Interfaces
{
    public interface IMasterDataServices
    {
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
    }
}
