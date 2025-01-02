using PoliceOfficerManagement.Data.Entity;

namespace PoliceOfficerManagement.Services.Employee.Interfaces
{
    public interface IEmployeeServices
    {
        Task<int> SaveEmployeeInfo(EmployeInfo model);
        Task<IEnumerable<EmployeInfo>> GetEmployeeInfo();
        Task<int> InActiveEmployeeById(int Id);
    }
}
