using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models
{
    public class RankWiseVacancyViewModel
    {
        public string rankName { get; set; }
        public int? totalPost { get; set; }
        public int? postedEmployee { get; set; }
        public int? totalVacant { get; set; }
    }
}
