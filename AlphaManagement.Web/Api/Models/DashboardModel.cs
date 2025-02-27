using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Api.Models
{
    public class DashboardModel
    {
        public int? onGoing { get; set; }
        public int? registration { get; set; }
        public int? finalSubmit { get; set; }
        public int? returned { get; set; }

        //internal Posting
        public int? internalongoing { get; set; }
        public int? internalApproved { get; set; }
        public int? internalReturn { get; set; }
    }
}
