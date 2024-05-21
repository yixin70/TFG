using Microsoft.AspNetCore.Mvc;

namespace TFG.Controllers
{
    public class TwitterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
