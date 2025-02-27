using AlphaManagement.DAL;
using AlphaManagement.Domain.EmployeeService.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.Domain.EmployeeService
{
    public class ApprovalMatrixService: IApprovalMatrixService
    {
        private readonly AlphaDbContext _context;

        public ApprovalMatrixService(AlphaDbContext context)
        {
            _context = context;
        }



    }
}
