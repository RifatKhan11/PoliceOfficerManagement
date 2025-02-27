using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.Organogram
{
    public class PhoneBookUnit:Base
    {
        public string Icon { get; set; }
        
        public string Name { get; set; }
        
        public int? statusId { get; set; }
        
        public int? parentId { get; set; }
        public PhoneBookUnit parent { get; set; }

        public int? Priority { get; set; }
        public int? jsonId { get; set; }
        public virtual ICollection<PhoneBook> PhoneBooks { get; set; }
    }
}
