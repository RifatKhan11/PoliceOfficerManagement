using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class Vaccines:Base
    {
        public int? vaccineGroupId { get; set; }
        public VaccineGroup vaccineGroup { get; set; }
        [Column(TypeName = "NVARCHAR(300)")]
        public string vaccineName { get; set; }
        [Column(TypeName = "NVARCHAR(300)")]
        public string vaccineNameBn { get; set; }
        public int? statusId { get; set; }
    }
}
