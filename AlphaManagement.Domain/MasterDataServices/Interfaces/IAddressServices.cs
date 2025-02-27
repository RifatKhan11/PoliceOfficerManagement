using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.MasterDataServices.Interfaces
{
    public interface IAddressServices
    {
        #region DivisionDistrict
        Task<IEnumerable<DivisionDistrict>> GetAllDivisionDistrict();
        Task<DivisionDistrict> GetAllDivisionDistrictById(int Id);
        #endregion

        #region Country
        Task<Country> GetCountryByCountryId(int CntId);
        #endregion

        #region Division
        Task<IEnumerable<Division>> GetAllDivision();
        Task<Division> GetDivisionById(int id);
        Task<IEnumerable<Division>> GetDivisionsByCountryId(int CntId);
        #endregion

        #region District
        Task<IEnumerable<PostOffice>> GetDistrictWiseThanaName();
        Task<IEnumerable<District>> GetAllDistrict();
        Task<District> GetDistrictById(int id);
        Task<IEnumerable<District>> GetDistrictsByDivisonId(int DivisionId);
        #endregion

        #region Upazilla
        Task<IEnumerable<Thana>> GetAllThana();
        Task<Thana> GetThanaById(int id);
        Task<IEnumerable<Thana>> GetThanasByDistrictId(int DistrictId);
        #endregion

        #region UnionWard
        Task<IEnumerable<UnionWard>> GetAllUnionWard();
        Task<IEnumerable<UnionWard>> GetUnionWardsByThanaId(int thanaId);
        #endregion

        #region Post Code
        Task<IEnumerable<PostOffice>> GetPostCodeByThanaId(int thanaId);
        #endregion

        #region Post In Unit
        Task<IEnumerable<PostInUnit>> GetPostInUnit();
        Task<PostInUnit> GetPostInUnitByRankAndUnit(int UnitId, int rankId);
        Task<IEnumerable<Section>> GetSection();
        Task<IEnumerable<Section>> GetSectionByFilter(int rankId, int unitId);
        #endregion

        #region MetropolitanArea
        Task<IEnumerable<MetropolitanArea>> GetAllMetroPolitanArea();
        #endregion
        #region PoliceUnit
        Task<IEnumerable<PoliceUnit>> GetAllPoliceUnit();
        #endregion   
        #region PoliceThana
        Task<IEnumerable<PoliceThana>> GetAllPoliceThana();
        #endregion
        #region ZoneCircle
        Task<IEnumerable<ZoneCircle>> GetAllZoneCircle();
        #endregion

        Task<AddressInformation> GetAllPresentAddress(int id);
        Task<AddressInformation> GetAllParmenantAddress(int id);
        Task<AddressInformation> GetInLawsAddress(int id);
        Task<AddressInformation> GetMeternalAddress(int id);
        Task<Photograph> GetSignutureById(int id);
    }
}
