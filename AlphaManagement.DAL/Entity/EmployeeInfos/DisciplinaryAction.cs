using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
    public class DisciplinaryAction:Base
    {
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? OffenseId { get; set; }
        public Offense Offense { get; set; }

        public int? naturalPunishmentId { get; set; }
        public NaturalPunishment naturalPunishment { get; set; }
        
        public DateTime? punishmentDate { get; set; }
        
        public DateTime? startingDate { get; set; }
        
        public DateTime? endDate { get; set; }
        public DateTime? goNumberWithDate { get; set; }

        public string goFileURL { get; set; }

        [Column(TypeName = "NVARCHAR(350)")]
        public string remarks { get; set; }
        public string OffenseName { get; set; }
        public string PunishmentName { get; set; }
        public string referenceNumber { get; set; }
        //-> Approved or Pending |  final status -> initiate or locked
        [MaxLength(20)]
        public string status { get; set; }
    }
}
