using AlphaManagement.DAL;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.MasterDataServices.Repository
{
  
    public class AddressServices: IAddressServices
    {
        private readonly AlphaDbContext _context;

        public AddressServices(AlphaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ZoneCircle>> GetAllZoneCircle()
        {
            return await _context.ZoneCircles
                .Include(x => x.divisionDistrict)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<PoliceThana>> GetAllPoliceThana()
        {
            return await _context.PoliceThanas
                .Include(x => x.rangeMetro)
                .Include(x => x.divisionDistrict)
                .Include(x => x.zoneCircle)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<PoliceUnit>> GetAllPoliceUnit()
        {
            return await _context.PoliceUnits
                .Include(x=>x.rangeMetro)
                .Include(x=>x.policeThana)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<MetropolitanArea>> GetAllMetroPolitanArea()
        {
            return await _context.MetropolitanAreas
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<District>> GetAllDistrict()
        {
            return await _context.Districts
                .Include(x => x.division)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Division>> GetAllDivision()
        {
            return await _context.Divisions
                .Include(x=>x.country)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Division> GetDivisionById(int id)
        {
            return await _context.Divisions.FindAsync(id);
        }

        public Task<IEnumerable<DivisionDistrict>> GetAllDivisionDistrict()
        {
            throw new NotImplementedException();
        }

        public Task<DivisionDistrict> GetAllDivisionDistrictById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Thana>> GetAllThana()
        {

           return await _context.Thanas.Include(x=>x.district).ToListAsync();
          
        }
        

        public Task<IEnumerable<Village>> GetAllVillageByThanaUnionId(int thana, int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Village>> GetAllVillageByUnionId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<District> GetDistrictById(int id)
        {
            return await _context.Districts.FindAsync(id);
        }

        public async Task<IEnumerable<District>> GetDistrictsByDivisonId(int DivisionId)
        {
            return await _context.Districts
                .Where(X => X.divisionId == DivisionId)
                .ToListAsync();
        }
        
        public async Task<Country> GetCountryByCountryId(int CntId)
        {
            return await _context.Countries.Where(X => X.Id == CntId).AsNoTracking().FirstOrDefaultAsync();
        }

        public Task<IEnumerable<Division>> GetDivisionsByCountryId(int CntId)
        {
            throw new NotImplementedException();
        }

        public Task<Thana> GetThanaById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Thana>> GetThanasByDistrictId(int DistrictId)
        {
            return await _context.Thanas
                .Where(x => x.districtId == DistrictId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PostOffice>> GetDistrictWiseThanaName()
        {
            return await _context.PostOffices
                .Include(x=>x.district)
                .Include(x=>x.thana)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<UnionWard>> GetUnionWardsByThanaId(int thanaId)
        {
            return await _context.UnionWards
                 .Where(x => x.thanaId == thanaId)
                 .ToListAsync();
        }

        public async Task<IEnumerable<PostOffice>> GetPostCodeByThanaId(int thanaId)
        {
            return await _context.PostOffices
                 .Where(x => x.thanaId == thanaId)
                 .ToListAsync();
        }

        public Task<IEnumerable<object>> GetVillageList()
        {
            throw new NotImplementedException();
        }

        #region Post In Unit
        public async Task<IEnumerable<PostInUnit>> GetPostInUnit()
        {
            return await _context.PostInUnits.Include(x => x.specialBranchUnit).Include(x => x.rank).ToListAsync();
        }
        public async Task<IEnumerable<Section>> GetSection()
        {
            return await _context.Sections.Include(x => x.specialBranchUnit).Include(x => x.rank).ToListAsync();
        }

        public async Task<IEnumerable<Section>> GetSectionByFilter(int rankId,int unitId)
        {
            return await _context.Sections.Include(x => x.specialBranchUnit).Include(x => x.rank).Where(x=>x.rankId==(rankId==0?x.rankId:rankId) && x.specialBranchUnitId == (unitId == 0 ? x.specialBranchUnitId : unitId)).ToListAsync();
        }


        public async Task<PostInUnit> GetPostInUnitByRankAndUnit(int UnitId , int rankId)
        {
            return await _context.PostInUnits.Include(x => x.specialBranchUnit).Include(x => x.rank).Where(x=> x.rankId == rankId && x.specialBranchUnitId == UnitId).FirstOrDefaultAsync();
        }


        #endregion


        public async Task<AddressInformation> GetAllParmenantAddress(int id)
        {
            var list = await _context.AddressInformation.Where(e=>e.employeeInfoId == id).Where(e => e.type == "Permanent Address").Include(e => e.employeeInfo).Include(x=>x.unionWard).Include(e => e.thana).Include(e => e.district).Include(e => e.division).AsNoTracking().FirstOrDefaultAsync();
            return list;
        }

        public async Task<AddressInformation> GetAllPresentAddress(int id)
        {
            var list = await _context.AddressInformation.Where(e=>e.employeeInfoId == id).Where(e => e.type == "Present Address").Include(e => e.employeeInfo).Include(x => x.unionWard).Include(e=>e.thana).Include(e=>e.district).Include(e=>e.division).AsNoTracking().FirstOrDefaultAsync();
            return list;
        }

        public async Task<AddressInformation> GetInLawsAddress(int id)
        {
            var list = await _context.AddressInformation.Where(e=>e.employeeInfoId == id).Where(e => e.type == "Spouse Address").Include(e => e.employeeInfo).Include(x => x.unionWard).Include(e=>e.thana).Include(e=>e.district).Include(e=>e.division).AsNoTracking().FirstOrDefaultAsync();
            return list;
        }

        public async Task<AddressInformation> GetMeternalAddress(int id)
        {
            var list = await _context.AddressInformation.Where(e=>e.employeeInfoId == id).Where(e => e.type == "Maternal Family Address").Include(e => e.employeeInfo).Include(x => x.unionWard).Include(e=>e.thana).Include(e=>e.district).Include(e=>e.division).AsNoTracking().FirstOrDefaultAsync();
            return list;
        }

        public async Task<Photograph> GetSignutureById(int id)
        {
            var list = await _context.Photographs.Where(e => e.type == "signature").Where(e=>e.employeeId == id).FirstOrDefaultAsync();
            return list;
        }

        public async Task<IEnumerable<UnionWard>> GetAllUnionWard()
        {
            return await _context.UnionWards.Include(a => a.thana.district.division).AsNoTracking().ToListAsync();
        }

    }
}
