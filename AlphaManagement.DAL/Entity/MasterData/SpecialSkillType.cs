using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.MasterData
{
   public class SpecialSkillType:Base
    {
        public string name { get; set; }
        public string nameBn { get; set; }
        public int? sortOrder { get; set; }
    }
}
