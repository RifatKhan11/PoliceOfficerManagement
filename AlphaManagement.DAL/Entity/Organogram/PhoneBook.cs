using AlphaManagement.DAL.Entity.MasterData;

namespace AlphaManagement.DAL.Entity.Organogram
{
    public class PhoneBook:Base
    {
        public string photo { get; set; }
        public int? unitId { get; set; }
        public PhoneBookUnit unit { get; set; }
        public int? rankId { get; set; }
        public PhoneBookRank rank { get; set; }
        public string rank_name { get; set; }
        public string designation_name { get; set; }
        public int? batch_bcs { get; set; }
        public string phone_office { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public int? jsonId { get; set; }
    }
}
