using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class Offense:Base
    {
        [Column(TypeName = "NVARCHAR(150)")]
        public string offense { get; set; }
        [Column(TypeName = "NVARCHAR(350)")]
        public string description { get; set; }
        public int? shortOrder { get; set; }
    }
}
