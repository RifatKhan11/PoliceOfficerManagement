using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.MasterDataServices.Interfaces
{
    public interface ISpecialBranchUnitServices
    {
        #region SpecialBranchUnit
        Task<IEnumerable<SpecialBranchUnit>> GetAllSpecialBranchUnit();
        Task<SpecialBranchUnit> GetSpecialBranchUnitById(int Id);
        #endregion
    }
}
