using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
    public class EmployeeMedicalVaccine:Base
    {
        public int? medicalId { get; set; }
        public MedicalInfo medical { get; set; }

        public int? vaccinesId { get; set; }
        public Vaccines vaccines { get; set; }
        [Column(TypeName = "NVARCHAR(250)")]
        public string remarks { get; set; }
    }
}
