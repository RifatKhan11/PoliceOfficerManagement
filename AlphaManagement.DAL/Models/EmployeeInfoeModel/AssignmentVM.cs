using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class AssignmentVM
    {
        public int employeeId { get; set; }
        public int assignmentid { get; set; }
        public string nameBangla { get; set; }
        public string employeeCode { get; set; }
        public string designation { get; set; }
        public string rank { get; set; }
        public int? drereeid { get; set; }
        public string section { get; set; }
        public string branch { get; set; }
    }


    public class AssignmentViewModels
    {
        public string nameBangla { get; set; }
        public string employeeCode { get; set; }
        public string currentPostingPlace { get; set; }
        public string homeDistrict { get; set; }
        public string joiningDate { get; set; }
        public string joiningDateCurrentPlace { get; set; }
        public string promotionDate { get; set; }
        public string dateOfBirth { get; set; }
        public string PreviousPostingPlace { get; set; }
        public string NewPostingPlace { get; set; }
        public string EducationQualification { get; set; }
        public string RankName { get; set; }
    }
}
