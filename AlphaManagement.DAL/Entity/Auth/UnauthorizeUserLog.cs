using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlphaManagement.DAL.Entity.Auth
{
   public class UnauthorizeUserLog:Base
    {
        [MaxLength(250)]
        public string userId { get; set; }
        [MaxLength(250)]
        public DateTime logTime { get; set; }
        public int? status { get; set; }
        [MaxLength(250)]
        public string ipAddress { get; set; }
        [MaxLength(350)]
        public string browserName { get; set; }
        [MaxLength(250)]
        public string pcName { get; set; }

        public string temptationString { get; set; }
        public string remarks { get; set; }
    }
}
