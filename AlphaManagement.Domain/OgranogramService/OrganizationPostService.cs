using AlphaManagement.DAL;
using AlphaManagement.DAL.Entity.Organogram;
using AlphaManagement.Domain.OgranogramService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.OgranogramService
{
   public class OrganizationPostService: IOrganizationPostService
    {
        private readonly AlphaDbContext _context;

        public OrganizationPostService(AlphaDbContext context)
        {
            _context = context;
        }

        #region Organization
        public async Task<bool> SaveOrganization(OrganoOrganization organoOrganization)
        {
            if (organoOrganization.Id != 0)
                _context.OrganoOrganizations.Update(organoOrganization);
            else
                _context.OrganoOrganizations.Add(organoOrganization);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrganoOrganization>> GetAllOrganization()
        {
            return await _context.OrganoOrganizations.Include(x => x.organizationType).Include(x => x.organoOrganization).AsNoTracking().ToListAsync();
        }

        public async Task<OrganoOrganization> GetOrganizationById(int id)
        {
            return await _context.OrganoOrganizations.Include(x => x.organizationType).Include(x => x.organoOrganization).Where(x => x.Id == id).FirstAsync();
        }

        public async Task<bool> DeleteOgnanoOgnanizationChildById(int id)
        {
            _context.OrganoOrganizations.RemoveRange(_context.OrganoOrganizations.Where(x=>x.organoOrganizationId==id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteOrganizationById(int id)
        {
            _context.OrganoOrganizations.Remove(_context.OrganoOrganizations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrganoOrganization>> GetRootOrganizations()
        {
            return await _context.OrganoOrganizations.Where(x => x.organoOrganizationId == null).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<OrganoOrganization>> GetOrganizationByParrentId(int parrentId)
        {
            return await _context.OrganoOrganizations.Where(x => x.organoOrganizationId == parrentId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<OrganoOrganization>> GetAllOrganizationByIds(List<int> ids)
        {
            return await _context.OrganoOrganizations.Where(x => ids.Contains(x.Id)).AsNoTracking().ToListAsync();
        }

        public List<int> GetllChildIdsByparrentId(int parrentId)
        {
            return _context.OrganoOrganizations.Where(x => x.organoOrganizationId == parrentId).Select(x => x.Id).ToList();
        }
        #endregion

        #region Post
        public async Task<bool> SavePost(Post post)
        {
            int tm = 0;
            if (post.Id != 0)
                _context.Posts.Update(post);
            else
                _context.Posts.Add(post);
            tm = await _context.SaveChangesAsync();

            if (tm != 0)  //Saving Post Details
            {
                //for (var i = 0; i < post.numberOfPost; i++)
                //{
                //    PostDetails postDetails = new PostDetails
                //    {
                //        postId = post.Id
                //    };
                //    await SavePostDetails(postDetails);
                //}
            }

            if (tm == 0)
                return false;

            return true;
        }

        public async Task<IEnumerable<Post>> GetAllPost()
        {
            return await _context.Posts.Include(x => x.organoOrganization).Include(x => x.designation).AsNoTracking().ToListAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<bool> DeletePostById(int id)
        {
            _context.Posts.Remove(_context.Posts.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetPostDetails(int orgId, int IsHead)
        {
            return await _context.Posts.Where(x => (x.organoOrganizationId == orgId && x.IsHead == IsHead)).Include(x => x.designation).Select(x => x.designation.designationName + "|" + x.numberOfPost).AsNoTracking().ToListAsync();
        }

        public async Task<string> GetAllPostString(int orgId)
        {
            List<Post> posts = await _context.Posts.Where(x => x.organoOrganizationId == orgId).Include(x => x.designation).Include(y => y.altDesignation).OrderByDescending(x => x.IsHead).AsNoTracking().ToListAsync();
            string Data = "";
            bool flag = false;
            foreach (Post post in posts)
            {
                if (flag) Data += "|";
                Data += post.numberOfPost.ToString() + "x" + post.designation.shortName;
                if (post.altDesignation != null) Data += "/" + post.altDesignation.shortName;
                flag = true;
            }
            return Data;
        }

        private List<int> GetPostIdsByOrgIds(List<int?> ids)
        {
            return _context.Posts.Where(x => ids.Contains(x.organoOrganizationId)).Select(x => x.Id).ToList();
        }

        public async Task<bool> SaveOrUpdatePost(Post post)
        {
            Post tm = await _context.Posts.Where(x => x.organoOrganizationId == post.organoOrganizationId).Where(x => x.designationId == post.designationId && x.altDesignationId == post.altDesignationId).FirstOrDefaultAsync();

            if (tm != null)
            {
                int cnt = _context.EmployeeInfos.Where(x => x.post == tm.Id).Count();
                if (cnt > post.numberOfPost) post.numberOfPost = cnt;
                tm.numberOfPost = post.numberOfPost;
                tm.IsHead = post.IsHead;
            }
            else _context.Posts.Add(post);

            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Recursion
        private List<int> myChilds(int id)
        {
            List<int> data = new List<int>();
            List<int> ids = this.GetllChildIdsByparrentId(id);
            data.AddRange(ids);
            foreach (int tm in ids) data.AddRange(this.myChilds(tm));
            return data;
        }
        #endregion

    }
}
