using Newtonsoft.Json;

namespace AlphaManagement.Web.Models.JsonModel
{
    public class JsonContact
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("photo")]
        public string photo { get; set; }
        [JsonProperty("unit_id")]
        public string unit_id { get; set; }
        [JsonProperty("rank")]
        public int rank { get; set; }
        [JsonProperty("rank_name")]
        public string rank_name { get; set; }
        [JsonProperty("designation_name")]
        public string designation_name { get; set; }
        [JsonProperty("batch_bcs")]
        public int batch_bcs { get; set; }
        [JsonProperty("phone_office")]
        public string phone_office { get; set; }
        [JsonProperty("telephone")]
        public string telephone { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("lastplace")]
        public string lastplace { get; set; }
    }
}
