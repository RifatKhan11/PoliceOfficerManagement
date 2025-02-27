using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.MasterData.Models
{
    public class BCSBatchViewModel
    {
        public int BCSBatchId { get; set; }
        public string batchName { get; set; }
        public string batchNameBn { get; set; }
        public int? shortOrder { get; set; }
        public IEnumerable<BCSBatch> bCSBatches { get; set; } 
        public IEnumerable<PHQTRType> pHQTRTypes { get; set; } 
        public BCSBatch bCSBatche { get; set; } 
    }
}
