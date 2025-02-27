using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models
{
    public class EmpolyeeDetailsByBCS
    {
        public int id { get; set; }
        public string bcsBatch { get; set; }
        public int? bcsPosition { get; set; }
        public string name { get; set; }
        public string bp { get; set; }
        public string fatherName { get; set; }
        public string rank { get; set; }
        public string motherName { get; set; }
        public string workingPlace { get; set; }
        public string address { get; set; }
    }
}
