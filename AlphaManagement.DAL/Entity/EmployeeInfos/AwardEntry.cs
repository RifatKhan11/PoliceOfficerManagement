using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
    public class AwardEntry:Base
    {
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
        [Column(TypeName = "NVARCHAR(250)")]
        public string awardName { get; set; }
        public int? awardId { get; set; }
        public Award award { get; set; }
        [Column(TypeName = "NVARCHAR(350)")]
        public string purpose { get; set; }
        public string referenceNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime awardDate { get; set; }

        //approver
        [Column(TypeName = "NVARCHAR(150)")]
        public string status { get; set; }

        public int? isAdminEntry { get; set; } //Ensure Admin Entry
    }
}
