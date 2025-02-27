using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Models;
using AlphaManagement.DAL.Models.Portfolio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.EmployeeService.interfaces
{
    public interface IPortfolioDashBoard
    {
        Task<EmployeeInfo> GetUserEmployeeInfo(string bpNo);
        Task<IEnumerable<ForeignTravel>> GetForeignTravelsById(string bpNo);
        Task<IEnumerable<Assignment>> GetAssignmentsByEmployeeCode(string bpNo);
        Task<EmployeeInfo> GetSupervisorInfo(string bpNo);
        Task<IEnumerable<Assignment>> GetAssignmentsForAdmin(); 
        Task<IEnumerable<Assignment>> GetApprovedA47ForAdmin();
        Task<IEnumerable<Assignment>> GetAssignmentsForSupervisor(int id);
        Task<IEnumerable<SearchEmployee_Sp>> GetSearchEmployeeInfo(string input, int rank, int unit, int batch);
        Task<IList<UnitWiseSectionPhoneBook>> GetUnitWiseSectionPhoneBook();
    }
}
