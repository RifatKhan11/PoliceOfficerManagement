using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.MasterData.Models.Lang
{
    public class SpecialBranchUnitLn
    {
        public string title { get; set; }
        public string branchUnitName { get; set; }
        public string branchUnitNameBN { get; set; }
        public string branchCode { get; set; }
        public string shortOrder { get; set; }
        public string isdefault { get; set; }
        public string action { get; set; }

        //Post In Unit Lang
        public string PostInUnittitle { get; set; }
        public string noOfPost { get; set; }
        public string PostName { get; set; }
        public string PostNameBN{ get; set; }


        public string rank { get; set; }
    }
}
