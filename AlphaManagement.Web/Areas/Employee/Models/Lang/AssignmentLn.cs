using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models.Lang
{
    public class AssignmentLn
    {
        public string Title { get; set; }
        public string employee { get; set; }
        public string assignmentType { get; set; }//1=Assignment,2=Transfer
        public string EntryNo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string policeUnit { get; set; }
        public string policeSubUnit { get; set; }
        public string Remarks { get; set; }
        public string action { get; set; }
    }
}
