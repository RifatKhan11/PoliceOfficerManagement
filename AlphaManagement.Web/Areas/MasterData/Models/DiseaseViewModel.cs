using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class DiseaseViewModel
    {
        public int diseasId { get; set; }
        public int groupId { get; set; }
        public int vaccineId { get; set; }
        public string diseaseName { get; set; }
        public string diseaseNameBn { get; set; }
     
        public string remarks { get; set; }
        public Disease disease { get; set; }
        public IEnumerable<Disease> diseases { get; set; }
        public IEnumerable<Vaccines> vaccines { get; set; }
        public IEnumerable<DiseaseGroup> diseaseGroups { get; set; }

        #region Vaccine 
        public int? vaccineGroupId { get; set; }
        public VaccineGroup vaccineGroup { get; set; }      
        public string vaccineName { get; set; }      
        public string vaccineNameBn { get; set; }
        public int? statusId { get; set; }
        #endregion
    }
}
