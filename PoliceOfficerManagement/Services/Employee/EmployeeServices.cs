using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PoliceOfficerManagement.Areas.EmployeeArea.Models;
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

        public async Task<IEnumerable<EmployeeInfoModel>> GetEmployeInfoSearch(int rangeId,int districtId,int zoneId, string name)
        {
            var data= await(from e in _context.employeeInfos join 
                            jr in _context.ranks on e.joiningRankId equals jr.Id

                            where e.nameEn == (name != null ? name : e.nameEn)
                            
                            select new EmployeeInfoModel
                            {
                                empName = e.nameEn,
                                empNameBn = e.nameEn,
                                employeeCode=e.employeeCode,
                                joiningRank =jr.rankName,
                                joiningDate=e.joiningDate,
                                currentPostingPlace= "",
                                currentRank="",
                                personalPhoneNumber=e.personalPhoneNumber,
                                homeDistrict=e.homeDistrict,
                                nidNumber=e.nidNumber,


                            }).ToListAsync();

            return data;
        }
        public async Task<EmployeeInfoModel> GetEmployeInfoById(int empId)
        {
            var data= await(from e in _context.employeeInfos join 
                            jr in _context.ranks on e.joiningRankId equals jr.Id

                            where e.Id == empId

                            select new EmployeeInfoModel
                            {
                                empName = e.nameEn,
                                empNameBn = e.nameEn,
                                employeeCode=e.employeeCode,
                                joiningRank =jr.rankName,
                                joiningDate=e.joiningDate,
                                retirmentDate = e.retirementDate,
                                currentPostingPlace= "",
                                currentRank="",
                                personalPhoneNumber=e.personalPhoneNumber,
                                homeDistrict=e.homeDistrict,
                                homeUpazila = e.homeUpazila,
                                nidNumber=e.nidNumber,
                                reMarks = e.reMarks,


                            }).FirstOrDefaultAsync();
            return data;
        }

        public async Task<IEnumerable<TrainingInfo>> GetTrainingInfoByEmpId(int empId)
        {
            var data = await _context.TrainingInfos.Where(x=>x.employeeId== empId).Include(x=>x.institute).ToListAsync();
            return data;
        }
        public async Task<IEnumerable<EducationalInfo>> GetEducationalInfoByEmpId(int empId)
        {
            var data=await _context.EducationalInfos.Where(x=>x.employeeId==empId).Include(x => x.institute).ToListAsync();
            return data;
        }
        public async Task<IEnumerable<AdderssInfo>> GetAdderssInfoByEmpId(int empId)
        {
            var data= await _context.AdderssInfos.Where(x=>x.employeeId==empId).Include(x=>x.division).Include(x=>x.district).Include(x=>x.thana).Include(x=>x.union).Include(x=>x.villege).ToListAsync();
            return data;
        }
        public async Task<IEnumerable<PostingPlace>> GetPostingPlaceByEmpId(int empId)
        {
            var data= await _context.PostingPlaces.Where(x => x.employeeId == empId).Include(x=>x.rank).Include(x=>x.range).Include(x=>x.district).Include(x=>x.zone).Include(x=>x.thana).ToListAsync();
            return data;
        }



    }
}
