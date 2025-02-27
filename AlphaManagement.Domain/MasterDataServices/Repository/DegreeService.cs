using AlphaManagement.DAL;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.MasterDataServices.Repository
{
    public class DegreeService : IDegreeService
    {
        private readonly AlphaDbContext _context;

        public DegreeService(AlphaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Degree>> GetDegree(int levelofeducationId)
        {
            return await _context.Degrees
                .Where(x => x.levelofeducationId == levelofeducationId)
                .AsNoTracking().OrderBy(x=>x.isDelete)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<RelDegreeSubject>> GetSubjectByDegreeId(int DegId)
        {
            return await _context.RelDegreeSubjects
                .Where(x => x.degreeId == DegId)
                .Include(x => x.subject)
                .ToListAsync();
        }

        public async Task<int> SaveSubjectInfo(Subject subject)
        {
            try
            {
                if (subject.Id != 0)
                {
                    _context.Subjects.Update(subject);
                }
                else
                {
                    _context.Subjects.Add(subject);
                }

                await _context.SaveChangesAsync();
                return subject.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveRelDegreeSubject(RelDegreeSubject relDegreeSubject)
        {
            try
            {
                if (relDegreeSubject.Id != 0)
                {
                    _context.RelDegreeSubjects.Update(relDegreeSubject);
                }
                else
                {
                    _context.RelDegreeSubjects.Add(relDegreeSubject);
                }

                await _context.SaveChangesAsync();
                return relDegreeSubject.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveOrganization(Organization organization)
        {
            try
            {
                if (organization.Id != 0)
                {
                    _context.Organizations.Update(organization);
                }
                else
                {
                    _context.Organizations.Add(organization);
                }

                await _context.SaveChangesAsync();
                return organization.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
