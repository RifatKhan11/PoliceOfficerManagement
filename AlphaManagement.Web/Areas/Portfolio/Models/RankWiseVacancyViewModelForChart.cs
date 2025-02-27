using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Portfolio.Models
{
    public class RankWiseVacancyViewModelForChart
    {
        public List<string> rankName { get; set; }
        public List<int?> totalVacant { get; set; }
    }
}
