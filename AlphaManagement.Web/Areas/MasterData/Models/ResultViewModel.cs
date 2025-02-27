using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class ResultViewModel
    {
        public int ResultId { get; set; }
        public string resultName { get; set; }
        public string resultNameBn { get; set; }
        public string resultShortName { get; set; }
        public decimal resultMaxValue { get; set; }
        public ResultLn fLang { get; set; }
        public Result result { get; set; }
        public IEnumerable<Result> results { get; set; }
    }
}
