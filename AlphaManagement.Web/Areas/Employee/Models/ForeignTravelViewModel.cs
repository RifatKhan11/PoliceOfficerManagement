using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class ForeignTravelViewModel
    {
        public int? ForeignTravelId { get; set; }
        public int? employeeId { get; set; }
        public int? countryId { get; set; }
        public string travelPurpose { get; set; }
        public string referenceNumber { get; set; }
        public string travelDuration { get; set; }
        public DateTime? travelDate { get; set; }
        public DateTime? travelEndDate { get; set; }
        public IEnumerable<ForeignTravel> foreignTravels { get; set;}
        public IEnumerable<Country> countries { get; set; }
    }
}
