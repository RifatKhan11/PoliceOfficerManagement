using AlphaManagement.DAL;
using AlphaManagement.DAL.Entity.Auth;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.MasterDataServices
{
    public class NavbarService: INavbarService
    {
        private readonly AlphaDbContext _context;

        public NavbarService(AlphaDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveNavbarItem(Navbar navbar)
        {
            if (navbar.Id != 0)
                _context.Navbars.Update(navbar);
            else
                _context.Navbars.Add(navbar);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
