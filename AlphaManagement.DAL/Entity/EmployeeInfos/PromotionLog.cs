using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
    public class PromotionLog:Base
    {
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public string designation { get; set; }

        public int? designationNewId { get; set; }
        public Designation designationNew { get; set; }

        public int? designationOldId { get; set; }
        public Designation designationOld { get; set; }

        public int? rankId { get; set; }
        public Rank rank { get; set; }

        public int? rankOldId { get; set; }
        public Rank rankOld { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }

        public int? payScaleId { get; set; }
        public SalaryGrade payScale { get; set; }

        public string goNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? goDate { get; set; }

        public string remark { get; set; }

        public int? isAdminEntry { get; set; } //Ensure Admin Entry
    }
}
