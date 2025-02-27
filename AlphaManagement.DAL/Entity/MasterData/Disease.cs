using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.MasterData
{
   public class Disease:Base
    {
        public int? diseaseGroupId { get; set; }
        public DiseaseGroup diseaseGroup { get; set; }
        [Required]
        [Column(TypeName ="NVARCHAR(300)")]
        public string diseaseName { get; set; }
        [Column(TypeName = "NVARCHAR(300)")]
        public string diseaseNameBn { get; set; }
        public int? status { get; set; }
        [Column(TypeName = "NVARCHAR(300)")]
        public string remarks { get; set; }

    }
}
