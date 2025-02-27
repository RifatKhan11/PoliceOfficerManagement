using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.RepositoryService.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Section> Sections { get; }
        //IGenericRepository<LeaveRequest> LeaveRequests { get;  }
        //IGenericRepository<LeaveAllocation> LeaveAllocations { get; }
        Task Save();
    }
}
