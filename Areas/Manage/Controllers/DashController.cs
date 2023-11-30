using Microsoft.AspNetCore.Mvc;

namespace TaskProject01._01._2023.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DashController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
