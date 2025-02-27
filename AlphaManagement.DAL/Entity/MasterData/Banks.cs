using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class Banks:Base
    {
        [Column(TypeName = "NVARCHAR(150)")]
        public string bankName { get; set; }
        [Column(TypeName = "NVARCHAR(150)")]
        public string bankNameBn { get; set; }
        public int? shortOrder { get; set; }
    }
}
