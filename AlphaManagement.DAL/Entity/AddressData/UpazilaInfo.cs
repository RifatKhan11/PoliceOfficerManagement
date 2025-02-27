using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.AddressData
{
    public class UpazilaInfo:Base
    {
        public int? districtId { get; set; }
        public DistrictInfo district { get; set; }
        [Column(TypeName = "NVARCHAR(120)")]
        public string name { get; set; }
        [Column(TypeName = "NVARCHAR(120)")]
        public string nameBn { get; set; }
        [Column(TypeName = "NVARCHAR(20)")]
        public string lat { get; set; }
        [Column(TypeName = "NVARCHAR(20)")]
        public string lon { get; set; }
        [Column(TypeName = "NVARCHAR(120)")]
        public string url { get; set; }
    }
}
