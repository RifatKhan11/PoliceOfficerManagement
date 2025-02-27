using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class AnulipiViewModel
    {
       
        public int anulipiId { get; set; }
        public string copyName { get; set; }
      
        public string copyNameBn { get; set; }

        public int? shortOrder { get; set; }

        public IEnumerable<AnulipiList> anulipiLists { get; set; }
    }
}
