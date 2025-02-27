using AlphaManagement.DAL.Entity.EmployeeInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class MedicalInfoViewModel
    {
        public int Id { get; set; }
        public int? isHospitalise { get; set; }
        public string year { get; set; }
        public string remarks { get; set; }
        public int? satatus { get; set; }
        public DateTime? date { get; set; }
        public int? isDelete { get; set; }
        public string lastCheckupHistory { get; set; }
        public EmployeeMedicalDisease medicalDisease { get; set; }
    }
}
