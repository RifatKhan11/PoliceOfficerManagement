using AlphaManagement.DAL.Entity.EmployeeInfos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class PhotographViewModel
    {
        public int employeeID { get; set; }

        public int photographID { get; set; }
        public int signatureID { get; set; }

        [Display(Name = "Employee Photo")]
        public IFormFile empPhoto { get; set; }

        [Display(Name = "Signature Photo")]
        public IFormFile signaturePhoto { get; set; }

       public Photograph photograph { get; set; }

        //public EmployeeSignature employeeSignature { get; set; }

        //public WagesPhotograph wagesPhotograph { get; set; }

        //public string employeeNameCode { get; set; }

        //public EmployeeInfoLn fLang { get; set; }

        //public EmployeeInfo employeeInfo { get; set; }

        //public WagesEmployeeInfo wagesEmployeeInfo { get; set; }

        //public string visualEmpCodeName { get; set; }
    }
}
