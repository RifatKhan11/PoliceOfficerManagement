using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class SpouseRelation:Base
    {
        [Column(TypeName = "nvarchar(100)")]
        public string relationName { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string relationNameBn { get; set; }
        public int? shortOrder { get; set; }
    }
}
