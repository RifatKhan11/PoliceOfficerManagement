using AlphaManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class EmpolyeeDetailsByBCSForPDF
    {
        public IEnumerable<EmpolyeeDetailsByBCS> empolyeeDetailsByBCS { get; set; }
        public IEnumerable<PMCorrectionDataModel> pMCorrectionDataModels { get; set; }
    }
}
