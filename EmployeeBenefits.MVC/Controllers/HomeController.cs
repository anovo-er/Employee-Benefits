using EmployeeBenefits.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeBenefits.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewApplication()
        {
            return View();
        }

        public IActionResult ApplicationDetails(int id)
        {
            ViewBag.ApplicationId = id;
            return View();
        }

        public IActionResult Employees()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}