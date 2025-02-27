using AlphaManagement.DAL.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.JWT_Service.Interfaces
{
   public interface IFilterRoleService
    {
        Task<IEnumerable<FileterRoleByUserViewModel>> GetFilterRoleByUserId(string id);
    }
}
