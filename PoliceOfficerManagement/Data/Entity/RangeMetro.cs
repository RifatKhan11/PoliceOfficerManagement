﻿using System.ComponentModel.DataAnnotations.Schema;

namespace PoliceOfficerManagement.Data.Entity
{
    public class RangeMetro : Base
    {
        [Column(TypeName = "NVARCHAR(350)")]
        public string rangeMetroName { get; set; }

        [Column(TypeName = "NVARCHAR(350)")]
        public string rangeMetroNameBn { get; set; }
        
        [Column(TypeName = "NVARCHAR(120)")]
        public string latitude { get; set; }

        [Column(TypeName = "NVARCHAR(120)")]
        public string longitude { get; set; }

        public int? pimsRangeId { get; set; }
        public string pimsRangeName { get; set; }        
    }
}
