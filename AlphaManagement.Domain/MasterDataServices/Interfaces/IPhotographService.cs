using AlphaManagement.DAL.Entity.EmployeeInfos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.MasterDataServices.Interfaces
{
   public interface IPhotographService
    {
        Task<bool> SavePhotograph(Photograph photograph);
        Task<Photograph> GetPhotographByType(int empId, string type);
        Task<bool> DeleteempId(int empId);
    }
}
