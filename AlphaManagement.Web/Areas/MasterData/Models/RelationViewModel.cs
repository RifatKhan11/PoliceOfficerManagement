using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class RelationViewModel
    {
        public int RelationId { get; set; }
        public string relationName { get; set; }
        public string relationNameBn { get; set; }
        public string relationShortName { get; set; }
        public RelationLn fLang { get; set; }
        public Relation relation { get; set; }
        public IEnumerable<Relation> relations { get; set; }
    }
}
