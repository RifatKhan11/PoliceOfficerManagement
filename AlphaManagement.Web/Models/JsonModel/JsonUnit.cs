using Newtonsoft.Json;

namespace AlphaManagement.Web.Models.JsonModel
{
    public class JsonUnit
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("parent_id")]
        public int ParentId { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }
    }
}
