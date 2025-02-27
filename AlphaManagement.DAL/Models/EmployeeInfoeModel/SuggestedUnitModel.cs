using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class SuggestedUnitModel
    {
        public int? subUnitId { get; set; }
        public string subUnitName { get; set; }
        public int? parentId { get; set; }
        public int? unitId { get; set; }
        public string unitName { get; set; }
        public int? districtsId { get; set; }
        public int? TotalPost { get; set; }
        public int? TotalEmployee { get; set; }
    }
}
