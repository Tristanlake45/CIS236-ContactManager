using Microsoft.AspNetCore.Mvc;
using PriceQuotation.Models;

namespace PriceQuotation.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Start state: empty input, all tips $0.00
            ViewBag.Tip10 = 0m;
            ViewBag.Tip15 = 0m;
            ViewBag.Tip20 = 0m;
            return View();
        }

        [HttpPost]
        public IActionResult Index(TipModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Tip10 = model.CalculateTip(10);
                ViewBag.Tip15 = model.CalculateTip(15);
                ViewBag.Tip20 = model.CalculateTip(20);
            }
            else
            {
                ViewBag.Tip10 = 0m;
                ViewBag.Tip15 = 0m;
                ViewBag.Tip20 = 0m;
            }

            return View(model);
        }
    }
}


