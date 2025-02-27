using AlphaManagement.DAL;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.MasterDataServices.Repository
{
  
    public class SpecialBranchUnitServices : ISpecialBranchUnitServices
    {
        private readonly AlphaDbContext _context;
        private IMemoryCache cache;

        public SpecialBranchUnitServices(AlphaDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            this.cache = memoryCache;
        }

        

        public async Task<IEnumerable<SpecialBranchUnit>> GetAllSpecialBranchUnit()
        {
            var data = new List<SpecialBranchUnit>();
            if (!cache.TryGetValue("SBU", out data))
            {
                data= await _context.SpecialBranchUnits.Include(x => x.districts).Include(x => x.specialBranchUnit).AsNoTracking().ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(100));

                cache.Set("SBU", data, cacheEntryOptions);
            }

            //var data = await _context.SpecialBranchUnits.Include(x=>x.districts).Include(x=>x.specialBranchUnit).AsNoTracking().ToListAsync();
            return data;
        }

        


        public async Task<SpecialBranchUnit> GetSpecialBranchUnitById(int Id)
        {
            return await _context.SpecialBranchUnits.Include(x=>x.specialBranchUnit).Where(x=> x.Id==Id).FirstOrDefaultAsync();
        }
    }
}
