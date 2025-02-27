using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.Auth
{
    public class ModuleAccessPage:Base
    {
        public int? alphaModuleId { get; set; }
        public AlphaModule alphaModule { get; set; }
        
        public string applicationRoleId { get; set; }
        public ApplicationRole applicationRole { get; set; }
    }
}
