using System;

namespace AlphaManagement.DAL.Entity.Auth
{
    public class MobileDeviceInformation:Base
    {
        public string deviceId { get; set; }
        public string imeiNo { get; set; }
        public string subscriptionNo { get; set; }
        public String ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string deviceLocation { get; set; }
        public string idAddress { get; set; }
        public int? statusId { get; set; }
    }
}
