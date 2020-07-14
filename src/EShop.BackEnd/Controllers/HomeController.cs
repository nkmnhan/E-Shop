using EShop.Shared.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace EShop.BackEnd.Controllers
{
    [ExcludeFromCodeCoverage]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVm { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
