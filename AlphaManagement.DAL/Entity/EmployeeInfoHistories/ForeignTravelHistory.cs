using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfoHistories
{
   public class ForeignTravelHistory:Base
    {
        public int? entryType { get; set; } //1=Admin;2=User

        public String UpdateUserId { get; set; }
        public ApplicationUser UpdateUser { get; set; }

        public int? foreignTravelId { get; set; }
        public ForeignTravel foreignTravel { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
        public int? countryId { get; set; }
        public Country country { get; set; }
        public string travelPurpose { get; set; }
        public DateTime? travelDate { get; set; }
        public DateTime? travelEndDate { get; set; }

        public int? status { get; set; }
        public string remarks { get; set; }
        public string travelDuration { get; set; }
        public string referenceNumber { get; set; }
    }
}
