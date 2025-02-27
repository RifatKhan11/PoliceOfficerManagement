using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Auth.Models
{
    public class UserListViewModel
    {
        public IEnumerable<ApplicationRoleViewModel> userRoles { get; set; }
    }
}
