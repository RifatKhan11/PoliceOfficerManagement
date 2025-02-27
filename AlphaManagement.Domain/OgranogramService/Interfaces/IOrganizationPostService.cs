using AlphaManagement.DAL.Entity.Organogram;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.OgranogramService.Interfaces
{
   public interface IOrganizationPostService
    {
        //Organization
        Task<bool> SaveOrganization(OrganoOrganization organoOrganizationorga);
        Task<IEnumerable<OrganoOrganization>> GetAllOrganization();
        Task<OrganoOrganization> GetOrganizationById(int id);
        Task<bool> DeleteOrganizationById(int id);
        Task<IEnumerable<OrganoOrganization>> GetRootOrganizations();
        Task<bool> DeleteOgnanoOgnanizationChildById(int id);
        Task<IEnumerable<OrganoOrganization>> GetOrganizationByParrentId(int parrentId);
        Task<IEnumerable<OrganoOrganization>> GetAllOrganizationByIds(List<int> ids);
        List<int> GetllChildIdsByparrentId(int parrentId);

        //Post
        Task<bool> SavePost(Post post);
        Task<IEnumerable<Post>> GetAllPost();
        Task<Post> GetPostById(int id);
        Task<bool> DeletePostById(int id);
        Task<string> GetAllPostString(int orgId);
        Task<List<string>> GetPostDetails(int orgId, int IsHead);
        Task<bool> SaveOrUpdatePost(Post post);
    }
}
