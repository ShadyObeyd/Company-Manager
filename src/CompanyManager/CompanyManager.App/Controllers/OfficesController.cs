using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.App.Controllers
{
    public class OfficesController : Controller
    {
        [HttpGet]
        public IActionResult CreateOffice()
        {
            return View();
        }
    }
}
