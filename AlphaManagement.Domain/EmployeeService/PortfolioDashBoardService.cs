using AlphaManagement.DAL;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Models;
using AlphaManagement.DAL.Models.Portfolio;
using AlphaManagement.Domain.EmployeeService.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.EmployeeService
{
    public class PortfolioDashBoardService : IPortfolioDashBoard
    {
        private readonly AlphaDbContext _context;
        public PortfolioDashBoardService(AlphaDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeInfo> GetUserEmployeeInfo(string bpNo)
        {
            var data = await _context.EmployeeInfos.Where(x => x.employeeCode == bpNo)
                        .Select(x => new EmployeeInfo {
                            employeeCode = x.employeeCode,
                            nameEnglish = x.nameEnglish,
                            nameBangla = x.nameBangla,
                            rank = x.rank,
                            designation = x.designation,
                            emailAddressPersonal = x.emailAddressPersonal,
                            emailAddress = x.emailAddress,
                            Id = x.Id,
                            dateOfBirth = x.dateOfBirth,
                            joiningDateGovtService = x.joiningDateGovtService,
                            joiningDatePresentWorkstation = x.joiningDatePresentWorkstation,
                            bCSBatch = x.bCSBatch,
                            bcsPosition = x.bcsPosition,
                            branch = x.branch,
                            updatedBy = _context.Photographs.Where(c => c.employeeId == x.Id && c.type == "profile").Select(y => y.url).FirstOrDefault()
                        }).FirstOrDefaultAsync();
            return data;
        }

        public async Task<IEnumerable<ForeignTravel>> GetForeignTravelsById(string bpNo)
        {
            var result = await _context.ForeignTravels
                .Include(x => x.country)
                .Include(x => x.employee)
                .Where(X => X.employee.employeeCode == bpNo && (X.status == 1 || X.status == 2))
                .OrderByDescending(x => x.travelDate)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByEmployeeCode(string bpNo)
        {
            var result = await _context.Assignments
                .Include(x => x.assignmentMaster)
                .Include(x => x.employee)
                .Include(x => x.employee.rank)
                .Include(x => x.specialBranchUnit)
                .Include(x => x.specialBranchUnit.specialBranchUnit)
                .Include(x => x.section)
                .Where(x => x.employee.employeeCode == bpNo)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsForAdmin()
        {
            var result = await _context.Assignments
                .Include(x => x.assignmentMaster)
                .Include(x => x.employee)
                .Include(x => x.employee.rank)
                .Include(x => x.supervisor)
                .Include(x => x.supervisor.rank)
                .Include(x => x.specialBranchUnit)
                .Include(x => x.specialBranchUnit.specialBranchUnit)
                .Include(x => x.section)
                .Where(x =>Convert.ToString(x.articleStatus) == "Ongoing")
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Assignment>> GetApprovedA47ForAdmin()
        {
            var result = await _context.Assignments
                .Include(x => x.assignmentMaster)
                .Include(x => x.employee)
                .Include(x => x.employee.rank)
                .Include(x => x.supervisor)
                .Include(x => x.supervisor.rank)
                .Include(x => x.specialBranchUnit)
                .Include(x => x.specialBranchUnit.specialBranchUnit)
                .Include(x => x.section)
                .Where(x => Convert.ToString(x.articleStatus) == "Approved" && x.statusId==3)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsForSupervisor(int id)
        {
            var result = await _context.Assignments
                .Include(x => x.assignmentMaster)
                .Include(x => x.employee)
                .Include(x => x.employee.rank)
                .Include(x => x.supervisor)
                .Include(x => x.supervisor.rank)
                .Include(x => x.specialBranchUnit)
                .Include(x => x.specialBranchUnit.specialBranchUnit)
                .Include(x => x.section)
                .Where(x => (Convert.ToString(x.articleStatus) == "Ongoing" || Convert.ToString(x.articleStatus) == "Approved" || Convert.ToString(x.articleStatus) == "Suberviser") && x.supervisorId==id)
                .ToListAsync();
            return result;
        }
        public async Task<EmployeeInfo> GetSupervisorInfo(string bpNo)
        {
            return await _context.EmployeeInfos.Include(x => x.rank).Where(x => x.employeeCode == bpNo).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SearchEmployee_Sp>> GetSearchEmployeeInfo(string input,int rank,int unit,int batch)
        {
            try
            {
                return await _context.searchEmployee_Sps.FromSql($"SP_GetSearchEmployeeInfo {input},{rank},{unit},{batch}").AsNoTracking().ToListAsync();
            }
            catch (Exception e)
            {

                throw;
            }
            
        }

        public async Task<IList<UnitWiseSectionPhoneBook>> GetUnitWiseSectionPhoneBook()
        {
            var data = new List<UnitWiseSectionPhoneBook>();
            var mainBranch = await _context.SpecialBranchUnits.Where(x => x.isParent == 1).ToListAsync();
            foreach (var mainUnit in mainBranch)
            {
                var subBranch = await _context.SpecialBranchUnits.Where(x => x.specialBranchUnitId == mainUnit.Id).ToListAsync();
                var departmentList = await _context.Departments.Where(x => x.section.specialBranchUnitId == mainUnit.Id).ToListAsync();
                var mainBranchh = new UnitWiseSectionPhoneBook
                {
                    unitName = mainUnit.branchUnitName,
                    mainBranchUnit = mainUnit
                };
                data.Add(mainBranchh);
                foreach (var department in departmentList)
                {
                    var mainBranchPhoneList = await _context.BDPolicePhoneBooks.Where(x => x.departmentId == department.Id).ToListAsync();
                    var mainBranchPhoneBook = new UnitWiseSectionPhoneBook
                    {
                        unitName = mainUnit.branchUnitName,
                        mainBranchUnit = mainUnit,
                        department = department,
                        departmentName= department.deptName,
                        phoneBooks = mainBranchPhoneList
                    };
                    data.Add(mainBranchPhoneBook);
                }                
                if (subBranch.Count() > 0)
                {
                    foreach (var subUnit in subBranch)
                    {
                        var subSectionList = await _context.Sections.Where(x => x.specialBranchUnitId == subUnit.Id).ToListAsync();
                        var subUnitPhoneBook = new UnitWiseSectionPhoneBook
                        {
                            subUnitName = subUnit.branchUnitName,
                            subBranchUnit = subUnit
                        };
                        data.Add(subUnitPhoneBook);
                        if (subSectionList.Count()>0)
                        {
                            foreach (var subSection in subSectionList)
                            {
                                var subBranchPhoneList = await _context.BDPolicePhoneBooks.Where(x => x.sectionId == subSection.Id).ToListAsync();
                                var subSectionPhoneBook = new UnitWiseSectionPhoneBook
                                {
                                    subUnitName = subUnit.branchUnitName,
                                    subBranchUnit = subUnit,
                                    section = subSection,
                                    sectionName = subSection.Name,
                                    phoneBooks = subBranchPhoneList
                                };
                                data.Add(subSectionPhoneBook);
                            }
                        }                      
                    }
                }
            }
            return data;
        }
    }
}
