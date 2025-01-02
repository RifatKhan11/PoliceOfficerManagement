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


    }
}
