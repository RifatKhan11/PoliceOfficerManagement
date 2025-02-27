using AlphaManagement.DAL;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.MasterDataServices.Repository
{
    public class PhotographService: IPhotographService
    {
        private readonly AlphaDbContext _context;

        public PhotographService(AlphaDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteempId(int empId)
        {
            _context.Photographs.RemoveRange(_context.Photographs.Where(a => a.employeeId == empId));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<Photograph> GetPhotographByType(int empId,string type)
        {
            var result = await _context.Photographs.Where(x => x.type == type && x.employeeId == empId).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> SavePhotograph(Photograph photograph)
        {
            if (photograph.Id != 0)
                _context.Photographs.Update(photograph);
            else
                _context.Photographs.Add(photograph);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
