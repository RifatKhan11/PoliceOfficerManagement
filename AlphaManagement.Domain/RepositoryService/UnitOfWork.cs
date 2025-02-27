using AlphaManagement.DAL;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using System;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AlphaDbContext _context;
        private  IGenericRepository<Section> _sections;

        

        //private  IGenericRepository<LeaveRequest> _leaveRequests;
        //private  IGenericRepository<LeaveAllocation> _leaveAllocations;

        public UnitOfWork(AlphaDbContext context)
        {
            _context = context;
        }
        public IGenericRepository<Section> Sections => _sections?? new GenericRepository<Section>(_context);
        //public IGenericRepository<Section> Sections
        //    => _sections ??= new GenericRepository<Section>(_context);

        //public IGenericRepository<Section> Sections()
        //{
        //    return _sections = _sections ?? new GenericRepository<Section>(_context);
        //}

        //public IGenericRepository<LeaveRequest> LeaveRequests
        //      => _leaveRequests ??= new GenericRepository<LeaveRequest>(_context);
        //public IGenericRepository<LeaveAllocation> LeaveAllocations
        //      => _leaveAllocations ??= new GenericRepository<LeaveAllocation>(_context);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if(dispose)
            {
                _context.Dispose();
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
