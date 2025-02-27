using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
    public class Assignment:Base
    {
        public int? assignmentMasterId { get; set; }
        public AssignmentMaster assignmentMaster { get; set; }
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? assignmentTypeId { get; set; }//1=Assignment,2=Transfer

        //public string assignmentTypeName { get; set; }

        public int? EntryNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public int? rankId { get; set; }
        public Rank rank { get; set; }

        public int? designationId { get; set; }
        public Designation designation { get; set; }

        public int? departmentId { get; set; }
        public Department department { get; set; }

        public int? specialBranchUnitId { get; set; }
        public SpecialBranchUnit specialBranchUnit { get; set; }

        public int? supervisorId { get; set; }
        public EmployeeInfo supervisor { get; set; }

        public string reasonOfTransfer { get; set; }
        public string ministryRefNo { get; set; }
        public string receiveRefNo { get; set; }

        public int? sectionId { get; set; }
        public Section section { get; set; }
        [Column(TypeName = "NVARCHAR(150)")]
        public string sectionName { get; set; }
        [Column(TypeName = "NVARCHAR(250)")]
        public string servicePeriod { get; set; }
        [Column(TypeName = "NVARCHAR(550)")]
        public string Remarks { get; set; }
        [Column(TypeName = "NVARCHAR(350)")]
        public string designationName { get; set; }
        [Column(TypeName = "NVARCHAR(350)")]
        public string unitName { get; set; }

        public int? statusId { get; set; }
        public ArticleType? articleType { get; set; }
        public DayTime? joinDayTime { get; set; }
        public DayTime? releaseDayTime { get; set; }
        public ArticleStatus? articleStatus { get; set; }
        public int? isAdminEntry { get; set; } //Ensure Admin Entry

    }

    public enum ArticleType
    {
        ToDepurture=1,
        TakingCharge=2
    }
    public enum DayTime
    {
        Morning = 1,
        Afternoon = 2
    }
    public enum ArticleStatus
    {
        Ongoing = 1,
        Suberviser = 2,
        Approved = 3,
        Rejected=4
    }
}
