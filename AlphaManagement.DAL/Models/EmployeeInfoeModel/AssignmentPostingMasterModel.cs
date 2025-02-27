using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class AssignmentPostingMasterModel
    {
        public IEnumerable<AssignmentPostingModel> assignmentPostingModels { get; set; }
        public IEnumerable<Section> sections { get; set; }
    }
}
