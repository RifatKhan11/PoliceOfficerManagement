using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class LanguageViewModel
    {
        public int LanguageId { get; set; }
        public string languageName { get; set; }
        public string languageNameBn { get; set; }
        public string languageShortName { get; set; }
        public int? shortOrder { get; set; }
        public LanguageLn fLang { get; set; }
        public Language Language { get; set; }
        public IEnumerable<Language> LanguageList { get; set; }
    }
}
