using AlphaManagement.DAL.Models;
using AlphaManagement.DAL.Models.Auth;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Api.Models
{
    public class PostingProposalModel
    {
        public IEnumerable<AssignmentDetailsModal> assignmentDetailsModals { get; set; }
        public IEnumerable<PostingReportView> postingReportViews { get; set; }
        public IEnumerable<AspNetUsersViewModel> aspNetUsersViewModels { get; set; }
    }
}
