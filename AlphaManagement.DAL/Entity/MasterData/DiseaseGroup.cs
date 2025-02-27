using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class DiseaseGroup:Base
    {
        [Column(TypeName = "NVARCHAR(300)")]
        public string groupName { get; set; }
        [Column(TypeName = "NVARCHAR(300)")]
        public string groupNameBn { get; set; }
        public int? shortOrder { get; set; }
        public int? statusId { get; set; }
    }
}
