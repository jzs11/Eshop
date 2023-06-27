using Microsoft.AspNetCore.Mvc;

namespace Eshop.Areas.HelloWorld
{
    [Area("HelloWorld")]
    public class MoneyMarketController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
