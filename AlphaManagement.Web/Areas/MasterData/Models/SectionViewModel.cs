using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class SectionViewModel
    {
        public int SectionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameBN { get; set; }
        public string shortName { get; set; }
        public int? shortOrder { get; set; }
        public SectionLn fLang { get; set; }
        public Section section { get; set; }
        public IEnumerable<Section> sections { get; set; }
    }
}
