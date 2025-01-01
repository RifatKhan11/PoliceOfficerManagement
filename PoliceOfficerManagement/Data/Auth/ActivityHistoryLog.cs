using System.ComponentModel.DataAnnotations.Schema;

namespace PoliceOfficerManagement.Data.Auth
{
    public class ActivityHistoryLog : Base
    {
        public int? logTypeId { get; set; }
        public ActivityLogType logType { get; set; }
        public int? actionId { get; set; }
        [Column(TypeName = "NVARCHAR(150)")]
        public string actionName { get; set; }
        [Column(TypeName = "NVARCHAR(250)")]
        public string description { get; set; }
        public string ipAddress { get; set; }
        public DateTime date { get; set; }
        public int? statusId { get; set; }//1==Ok
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
