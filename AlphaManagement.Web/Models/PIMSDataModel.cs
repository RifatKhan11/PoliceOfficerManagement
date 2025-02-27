using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Models
{
    public class PIMSBody
    {
        public List<PIMSDataModel> items { get; set; }
    }
    public class PIMSDataModel
    {
        public string bp { get; set; }
        public string bangla_name { get; set; }
        public string english_name { get; set; }
        public string father_name { get; set; }
        public string mother_name { get; set; }
        public string spouse_name { get; set; }
        public string sex { get; set; }
        public string date_of_birth { get; set; }
        public string religion_name { get; set; }
        public string marital_status { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string present_rank { get; set; }
        public string police_joining_rank { get; set; }
        public string date_of_joining { get; set; }
        public string retirement_date { get; set; }
        public string employee_status { get; set; }
        public string current_place_of_posting { get; set; }
        public string main_unit { get; set; }
        public string unit { get; set; }
        public string sub_unit { get; set; }
        public string sub_sub_unit { get; set; }
        public string national_id { get; set; }
        public string home_district { get; set; }
        public string recognitionmarks { get; set; }
        public string picture { get; set; }
    }
}
