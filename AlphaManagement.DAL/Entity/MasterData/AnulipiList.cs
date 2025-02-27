using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class AnulipiList:Base
    {
        [Column(TypeName = "NVARCHAR(350)")]
        public string copyName { get; set; }
        [Column(TypeName = "NVARCHAR(350)")]
        public string copyNameBn { get; set; }
        public int? shortOrder { get; set; }
    }
}
