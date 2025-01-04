using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PoliceOfficerManagement.Data;
using PoliceOfficerManagement.Data.Entity;
using PoliceOfficerManagement.Services.Dapper.IInterfaces;
using PoliceOfficerManagement.Services.Employee.Interfaces;

namespace PoliceOfficerManagement.Services.Employee
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IDapper _dapper;
        public EmployeeServices(AppDbContext context, RoleManager<IdentityRole> roleManager, IDapper dapper)
        {
            _context = context;
            this.roleManager = roleManager;
            this._dapper = dapper;
        }
        public async Task<int> SaveEmployeeInfo(EmployeInfo model)
        {
            try
            {
                if (model.Id != 0)
                {
                    _context.employeeInfos.Update(model);
                    await _context.SaveChangesAsync();
                    return model.Id;
                }
                else
                {
                    await _context.employeeInfos.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return model.Id;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }


        }

        public async Task<int> SaveEmployeeOtherInfo(List<EducationalInfo> edu, List<TrainingInfo> train, List<PostingPlace> post, List<AdderssInfo> add)
        {
            try 
            {
                if (edu.Count > 0)
                {
                  await  _context.EducationalInfos.AddRangeAsync(edu);
                }
                if (train.Count > 0)
                {
                    await _context.TrainingInfos.AddRangeAsync(train);
                }
                if(post.Count > 0)
                {
                    await _context.AddRangeAsync(post);
                }
                if (add.Count > 0)
                {
                    await _context.AddRangeAsync(add);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<IEnumerable<EmployeInfo>> GetEmployeeInfo()
        {
            return await _context.employeeInfos.Where(x=>x.isActive != true).ToListAsync();
        }
        public async Task<int> InActiveEmployeeById(int Id)
        {
            var data = await _context.employeeInfos.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (data != null)
            {
                data.isActive = true;
                _context.employeeInfos.Update(data);
                await _context.SaveChangesAsync();


            }
            return 0;
        }



    }
}
