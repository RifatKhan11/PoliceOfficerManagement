using PoliceOfficerManagement.Data.Entity;

namespace PoliceOfficerManagement.Services.Employee.Interfaces
{
    public interface IEmployeeServices
    {
        Task<int> SaveEmployeeInfo(EmployeInfo model);
        Task<int> SaveEmployeeOtherInfo(List<EducationalInfo> edu, List<TrainingInfo> train, List<PostingPlace> post, List<AdderssInfo> add);
        Task<IEnumerable<EmployeInfo>> GetEmployeeInfo();
        Task<int> InActiveEmployeeById(int Id);
    }
}
