using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models
{
   public class AssignmentDetailsModal
    {
        public int? masterId { get; set; }
        public int? Id { get; set; }
        public int? employeeId { get; set; }
        public int? statusId { get; set; }
        public int? rankId { get; set; }
        public string reqNo { get; set; }
        public string creator { get; set; }
        public string rankName { get; set; }
        public string remarks { get; set; }
        public DateTime?  date { get; set; }
        public string picture { get; set; }
        public string Bp { get; set; }
        public string Name { get; set; }
        public string educationalQualification { get; set; }
        public string homeDistrict { get; set; }
        public string batch { get; set; }
        public string currentPostringPlace { get; set; }
        public string workingPeriod { get; set; }
        public string NewPostringPlace { get; set; }
        public DateTime? LastPromotionDate { get; set; }
        public DateTime? joiningDateOfPresentunit { get; set; }
        public DateTime? dateOfBirth { get; set; }
    }
}
