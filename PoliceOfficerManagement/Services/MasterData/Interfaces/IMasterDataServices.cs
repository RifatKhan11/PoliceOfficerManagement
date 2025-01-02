using PoliceOfficerManagement.Data.Entity;

namespace PoliceOfficerManagement.Services.MasterData.Interfaces
{
    public interface IMasterDataServices
    {
        Task<int> SaveRank(Rank model);
        Task<IEnumerable<Rank>> GetRank();
        Task<int> InActiveRankById(int Id);
    }
}
