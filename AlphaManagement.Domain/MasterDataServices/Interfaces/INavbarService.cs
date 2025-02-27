using AlphaManagement.DAL.Entity.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.MasterDataServices.Interfaces
{
    public interface INavbarService
    {
        Task<bool> SaveNavbarItem(Navbar navbar);
    }
}
