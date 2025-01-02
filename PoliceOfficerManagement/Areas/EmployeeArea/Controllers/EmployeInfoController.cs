using Microsoft.AspNetCore.Mvc;

namespace PoliceOfficerManagement.Areas.EmployeeArea.Controllers
{
    public class EmployeInfoController : Controller
    {
        public IActionResult CreateEmployeeProfile()
        {
            return View();
        }
    }
}
