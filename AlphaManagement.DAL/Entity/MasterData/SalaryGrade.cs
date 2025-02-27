using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class SalaryGrade:Base
    {
        [MaxLength(100)]
        public string gradeName { get; set; }

        public decimal? basicAmount { get; set; }

        [MaxLength(100)]
        public string payScale { get; set; }
        public decimal? amount { get; set; }

        [MaxLength(100)]
        public string type { get; set; }

        public decimal? currentBasic { get; set; }
    }
}
