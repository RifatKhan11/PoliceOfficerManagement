using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
    public class BankInformation:Base
    {
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
        public int? banksId { get; set; }
        public Banks banks { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string accountNo { get; set; }
        public int? accountType { get; set; }
    }
}
