using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.AddressData
{
   public class PostInUnit:Base 
    {
        public int? specialBranchUnitId { get; set; }
        public SpecialBranchUnit specialBranchUnit { get; set; }

        public int? rankId { get; set; }
        public Rank rank { get; set; }

        public int? numOfPost { get; set; }

        public int? status { get; set; }

        public string remarks { get; set; }
    }
}
