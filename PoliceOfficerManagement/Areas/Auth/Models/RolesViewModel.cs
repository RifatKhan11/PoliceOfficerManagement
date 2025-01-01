using Microsoft.AspNetCore.Identity;

namespace PoliceOfficerManagement.Areas.Auth.Models
{
    public class RolesViewModel
    {
        public int Id { get; set; }
        public string? name { get; set; }
        public int? statusId { get; set; }

        public IEnumerable<IdentityRole> identityRoles { get; set; }
    }
}
