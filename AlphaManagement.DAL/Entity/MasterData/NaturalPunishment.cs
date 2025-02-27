using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class NaturalPunishment:Base
    {
        [Column(TypeName = "NVARCHAR(250)")]
        public string name { get; set; }
        [Column(TypeName = "NVARCHAR(350)")]
        public string description { get; set; }
        public int? shortOrder { get; set; }
    }
}
