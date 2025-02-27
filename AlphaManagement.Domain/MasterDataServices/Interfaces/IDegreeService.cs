using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.MasterDataServices.Interfaces
{
   public interface IDegreeService
    {
        Task<IEnumerable<Degree>> GetDegree(int levelofeducationId);
        Task<IEnumerable<RelDegreeSubject>> GetSubjectByDegreeId(int DegId);
        Task<int> SaveSubjectInfo(Subject subject);
        Task<int> SaveRelDegreeSubject(RelDegreeSubject relDegreeSubject);
        Task<int> SaveOrganization(Organization organization);
       
    }
}
