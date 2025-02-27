using AlphaManagement.DAL.Entity.Auth;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Auth.Models
{
    public class ApplicationRoleViewModel
    {
        public string RoleId { get; set; }
        public string PreRoleId { get; set; }
        public string[] roleIdList { get; set; }

        public string userId { get; set; }

        public string userName { get; set; }
        public string EuserName { get; set; }

        public string RoleName { get; set; }

        public int? moduleId { get; set; }

        public string description { get; set; }

        public string moduleName { get; set; }
        public IEnumerable<AlphaModule> alphaModules { get; set; }
        public IEnumerable<ApplicationRoleViewModel> roleViewModels { get; set; }
        public IEnumerable<EmployeeInfo> userInfos { get; set; }
        public IEnumerable<Rank> ranks { get; set; }
        //public IEnumerable<Role> userInfos { get; set; }
    }
}
