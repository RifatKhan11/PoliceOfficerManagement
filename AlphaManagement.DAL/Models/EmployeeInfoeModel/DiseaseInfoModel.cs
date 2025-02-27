using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class DiseaseInfoModel
    {
        public int? medicalId { get; set; }
        public int? diseaseId { get; set; }
        public int? vaccineId { get; set; }
        public string diseaseName { get; set; }
        public string vaccineName { get; set; }
    }
}
