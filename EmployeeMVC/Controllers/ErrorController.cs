using Microsoft.AspNetCore.Mvc;

namespace EmployeeMVC.Controllers;

public class ErrorController : Controller
{
        [Route("Unauthorized")]
        public IActionResult Unauthorized()
        {
            return View();
        }

        [Route("Forbidden")]
        public IActionResult Forbidden()
        {
            return View();
        }
}
