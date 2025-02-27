using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
   public class MedicalInfo:Base
    {
        
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public DateTime? date { get; set; }
        public DateTime? recoverydate { get; set; }
        public DateTime? checkUpDate { get; set; }

        public int? isHospitalise { get; set; }

        public string hospitalName { get; set; }

        public string referanceDoctor { get; set; }
        public string year { get; set; }
        public string CVRMedicalInjury { get; set; }

        public string lastCheckupHistory { get; set; }

        public int? satatus { get; set; }
        public string remarks { get; set; }
        [NotMapped]
        public IEnumerable<EmployeeMedicalDisease> medicalDiseases { get; set; }
        [NotMapped]
        public IEnumerable<EmployeeMedicalVaccine> medicalVaccines { get; set; }
    }
}
