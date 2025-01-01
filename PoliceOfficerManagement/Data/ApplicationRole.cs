﻿using Microsoft.AspNetCore.Identity;

namespace PoliceOfficerManagement.Data
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName)
        {
            //this.moduleId = moduleId;
            //this.description = description;
        }
    }
}
