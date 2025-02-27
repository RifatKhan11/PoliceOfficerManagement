using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.Auth
{
    public class UserLogHistory:Base
    {
        [MaxLength(250)]
        public string userId { get; set; }
        [MaxLength(250)]
        public DateTime logTime { get; set; }
        public int? status { get; set; }
        [MaxLength(250)]
        public string ipAddress { get; set; }
        [MaxLength(250)]
        public string browserName { get; set; }
        [MaxLength(250)]
        public string pcName { get; set; }
        [NotMapped]
        public string statusName { get; set; }
    }
}
