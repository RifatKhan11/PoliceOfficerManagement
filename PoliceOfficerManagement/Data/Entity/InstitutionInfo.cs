using System.ComponentModel.DataAnnotations;

namespace PoliceOfficerManagement.Data.Entity
{
    public class InstitutionInfo : Base
    {
        public int? type { get; set; }// home,abroad
        
        [StringLength(400)]
        public string nameBn { get; set; }

        [StringLength(400)]
        public string nameEn { get; set; }

        [StringLength(50)]
        public string establishYear { get; set; }

        [StringLength(420)]
        public string placeInfo { get; set; }
    }
}
