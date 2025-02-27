using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class BanksViewModel
    {
        public int BanksId { get; set; }
        public string bankName { get; set; }
        public string bankNameBn { get; set; }
        public int? shortOrder { get; set; }
        public Banks bank { get; set; }
        public IEnumerable<Banks> banks { get; set; }
    }
}
