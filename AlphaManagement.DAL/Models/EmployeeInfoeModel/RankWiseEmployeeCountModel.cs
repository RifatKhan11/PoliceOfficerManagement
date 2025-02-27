using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class RankWiseEmployeeCountModel
    {
        public int count { get; set; }
        public int? rankId { get; set; }
        public string rankName { get; set; }
        public int? shortOrder { get; set; }
        public List<string> allRanks { get; set; }
        public List<int> noOfEmployees { get; set; }
        public List<int> rankIds { get; set; }
    }

    public class BatchWiseEmployeeCountModel
    {
        public int count { get; set; }
        public int? batchId { get; set; }
        public string batchName { get; set; }
        public List<string> allBatches { get; set; }
        public List<int> noOfEmployees { get; set; }
        public List<int> batchIds { get; set; }
    }
}
