using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class DiseasesViewModel
    {
        public int? diseaseGroupId { get; set; }
       
        public string diseaseName { get; set; }
      
        public string diseaseNameBn { get; set; }
        public int? status { get; set; }
   
        public string remarks { get; set; }
    }
}
