using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.DAL.Models.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.InternalPosting.Models
{
    public class InternalPostingReportViewModel
    {
        public IEnumerable<PostingReportView> postingReportViews { get; set; }
    }
}
