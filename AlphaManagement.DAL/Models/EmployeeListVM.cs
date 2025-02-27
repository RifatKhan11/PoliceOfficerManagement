using AlphaManagement.DAL.Entity.EmployeeInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models
{
   public class EmployeeListVM
    {
        public int? Id { get; set; }
        public string name { get; set; }
        public string rank { get; set; }
        public string designation { get; set; }
        public string employeeCode { get; set; }
        public Photograph photograph { get; set; }
        public string unit { get; set; }
        public string url { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string linktin { get; set; }
        public string homeDistrict { get; set; }
        public string bcsBatch { get; set; }
        public DateTime? joiningDate { get; set; }
        public DateTime? PRLDate { get; set; }
    }
}
