using AlphaManagement.DAL.Entity.AddressData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class Organization : Base
    {
        [Required]
        [Column(TypeName = "NVARCHAR(250)")]
        public string organizationName { get; set; }
        [Column(TypeName = "NVARCHAR(250)")]
        public string organizationNameBn { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(150)")]
        public string organizationType { get; set; }
        public int? shortOrder { get; set; }
        public int? countryId { get; set; }
        //public Country country { get; set; }
    }
}
