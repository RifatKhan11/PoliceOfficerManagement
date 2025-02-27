using System;

namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class GetPortfolioTransectionHistoryLog
    {
        public DateTime? transectionDate { get; set; }
        public string statusName { get; set; }
        public string empName { get; set; }
        public string remarks { get; set; }
        public string userName { get; set; }
        public string Status { get; set; }
    }
}
