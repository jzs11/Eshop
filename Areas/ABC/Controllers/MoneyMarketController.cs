using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
   [Area("ABC")]
   public class MoneyMarketController : Controller
   {
      public ActionResult Index()
      {
         return View();
      }
   }
}
