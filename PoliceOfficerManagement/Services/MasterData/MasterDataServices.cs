using Microsoft.AspNetCore.Identity;
using PoliceOfficerManagement.Data.Entity;
using PoliceOfficerManagement.Data;
using PoliceOfficerManagement.Services.Dapper.IInterfaces;
using PoliceOfficerManagement.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PoliceOfficerManagement.Services.MasterData
{
    public class MasterDataServices : IMasterDataServices
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IDapper _dapper;
        public MasterDataServices(AppDbContext context, RoleManager<IdentityRole> roleManager, IDapper dapper)
        {
            _context = context;
            this.roleManager = roleManager;
            this._dapper = dapper;
        }



        #region InstitutionInfo
        public async Task<int> SaveInstitutionInfo(InstitutionInfo model)
        {
            try
            {
                if (model.Id != 0)
                {
                    _context.InstitutionInfos.Update(model);
                    await _context.SaveChangesAsync();
                    return model.Id;
                }
                else
                {
                    await _context.InstitutionInfos.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return model.Id;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }


        }

        public async Task<IEnumerable<InstitutionInfo>> GetInstitutionInfo()
        {
            return await _context.InstitutionInfos.Where(x => x.instituteType == 1 && x.isActive != true).ToListAsync();
        }
        public async Task<IEnumerable<InstitutionInfo>> GetInstitutionInfoTraning()
        {
            return await _context.InstitutionInfos.Where(x => x.instituteType == 2 && x.isActive != true).ToListAsync();
        }
        public async Task<int> InActiveInstitutionInfoById(int Id)
        {
            var data = await _context.InstitutionInfos.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (data != null)
            {
                data.isActive = true;
                _context.InstitutionInfos.Update(data);
                await _context.SaveChangesAsync();


            }
            return 0;
        }

        #endregion

        #region Rank
        public async Task<int> SaveRank(Rank model)
        {
            try
            {
                if (model.Id != 0)
                {
                    _context.ranks.Update(model);
                    await _context.SaveChangesAsync();
                    return model.Id;
                }
                else
                {
                    await _context.ranks.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return model.Id;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }


        }

        public async Task<IEnumerable<Rank>> GetRank()
        {
            return await _context.ranks.Where(x => x.isActive != true).ToListAsync();
        }
        public async Task<int> InActiveRankById(int Id)
        {
            var data = await _context.ranks.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (data != null)
            {
                data.isActive = true;
                _context.ranks.Update(data);
                await _context.SaveChangesAsync();


            }
            return 0;
        }

        #endregion

        #region Division

        public async Task<IEnumerable<Division>> GetAllDivision()
        {
            var data = await _context.Divisions.ToListAsync();
            return data;
        }
        #endregion

        #region Districts
        public async Task<IEnumerable<District>> GetDistrictsByDivisionId(int divisionId)
        {
            var data = await _context.Districts.Where(x=>x.divisionId == divisionId).ToListAsync();
            return data;
        }
        #endregion
        #region Thanas
        public async Task<IEnumerable<Thana>> GetThanasByDistrictId(int districtId)
        {
            var data = await _context.Thanas.Where(x => x.districtId == districtId).ToListAsync();
            return data;
        }
        
        #endregion
        #region UnionWard
        public async Task<IEnumerable<UnionWard>> GetUnionWardsByThanaId(int thanaId)
        {
            var data = await _context.UnionWards.Where(x => x.thanaId == thanaId).ToListAsync();
            return data;
        }
        #endregion
        #region Village
        public async Task<IEnumerable<Village>> GetUnionWardsByUnionWardId(int unionWardId)
        {
            var data = await _context.Villages.Where(x => x.unionWardId == unionWardId).ToListAsync();
            return data;
        }
        #endregion
        #region Institution Info
        public async Task<IEnumerable<InstitutionInfo>> GetAllInstitutionInfoForTraning()
        {
            var data = await _context.InstitutionInfos.Where(x=>x.instituteType == 2 && x.isActive != true).ToListAsync();
            return data;
        }
        public async Task<IEnumerable<InstitutionInfo>> GetAllInstitutionInfo()
        {
            var data = await _context.InstitutionInfos.Where(x => x.instituteType == 1 && x.isActive != true).ToListAsync();
            return data;
        }
        #endregion
        #region RangeMetros
        public async Task<IEnumerable<RangeMetro>> GetAllRangeMetros()
        {
            var data = await _context.RangeMetros.Where(x => x.isActive != true).ToListAsync();
            return data;
        }
        public async Task<int> SaveRangeMetro(RangeMetro model)
        {
            try
            {
                if (model.Id != 0)
                {
                    _context.RangeMetros.Update(model);
                    await _context.SaveChangesAsync();
                    return model.Id;
                }
                else
                {
                    await _context.RangeMetros.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return model.Id;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }


        }

        
        public async Task<int> InActiveRangeMetroById(int Id)
        {
            var data = await _context.RangeMetros.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (data != null)
            {
                data.isActive = true;
                _context.RangeMetros.Update(data);
                await _context.SaveChangesAsync();


            }
            return 0;
        }

        #endregion
        #region Division District
        public async Task<IEnumerable<DivisionDistrict>> GetDivisionDistrictByRangeId(int rangeId)
        {
            var data = await _context.DivisionDistricts.Where(x=>x.rangeMetroId == rangeId).ToListAsync();
            return data;
        }


        public async Task<IEnumerable<DivisionDistrict>> GetAllDivisionDistrict()
        {
            var data = await _context.DivisionDistricts.Where(x => x.isActive != true).Include(x=>x.rangeMetro).ToListAsync();
            return data;
        }
        public async Task<int> SaveDivisionDistrict(DivisionDistrict model)
        {
            try
            {
                if (model.Id != 0)
                {
                    _context.DivisionDistricts.Update(model);
                    await _context.SaveChangesAsync();
                    return model.Id;
                }
                else
                {
                    await _context.DivisionDistricts.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return model.Id;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }


        }


        public async Task<int> InActiveDivisionDistrictById(int Id)
        {
            var data = await _context.DivisionDistricts.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (data != null)
            {
                data.isActive = true;
                _context.DivisionDistricts.Update(data);
                await _context.SaveChangesAsync();


            }
            return 0;
        }
        #endregion
        #region Zone Circle
        public async Task<IEnumerable<ZoneCircle>> GetZoneCircleByDivisionDistrictId(int divisionDistrictId)
        {
            var data = await _context.ZoneCircles.Where(x => x.divisionDistrictId == divisionDistrictId).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<ZoneCircle>> GetAllZoneCircle()
        {
            var data = await _context.ZoneCircles.Where(x => x.isActive != true).Include(x=>x.divisionDistrict).ToListAsync();
            return data;
        }
        public async Task<int> SaveZoneCircle(ZoneCircle model)
        {
            try
            {
                if (model.Id != 0)
                {
                    _context.ZoneCircles.Update(model);
                    await _context.SaveChangesAsync();
                    return model.Id;
                }
                else
                {
                    await _context.ZoneCircles.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return model.Id;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }


        }


        public async Task<int> InActiveZoneCircleById(int Id)
        {
            var data = await _context.ZoneCircles.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (data != null)
            {
                data.isActive = true;
                _context.ZoneCircles.Update(data);
                await _context.SaveChangesAsync();


            }
            return 0;
        }
        #endregion

        #region Police Thanas
         
        public async Task<IEnumerable<PoliceThana>> GetThanasByRangeId(int zoneId)
        {
            var data = await _context.PoliceThanas.Where(x => x.zoneCircleId == zoneId).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<PoliceThana>> GetAllPoliceThana()
        {
            var data = await _context.PoliceThanas
                                    .Where(x => x.isActive != true)
                                    .Include(x=>x.policeThana)
                                    .Include(x=>x.rangeMetro)
                                    .Include(x=>x.divisionDistrict)
                                    .Include(x=>x.zoneCircle)
                                    .Include(x=>x.upazilla)
                                    .ToListAsync();
            return data;
        }
        public async Task<int> SavePoliceThana(PoliceThana model)
        {
            try
            {
                if (model.Id != 0)
                {
                    _context.PoliceThanas.Update(model);
                    await _context.SaveChangesAsync();
                    return model.Id;
                }
                else
                {
                    await _context.PoliceThanas.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return model.Id;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }


        }


        public async Task<int> InActivePoliceThanaById(int Id)
        {
            var data = await _context.PoliceThanas.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (data != null)
            {
                data.isActive = true;
                _context.PoliceThanas.Update(data);
                await _context.SaveChangesAsync();


            }
            return 0;
        }
        #endregion
    }
}
