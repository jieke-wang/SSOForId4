using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSite2.Models;

namespace WebSite2.Controllers
{
    [Authorize()]
    public class HomeController : Controller
    {
        // 允许匿名访问
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        // 需要授权访问
        public IActionResult Privacy()
        {
            return View();
        }

        // 允许匿名访问
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
