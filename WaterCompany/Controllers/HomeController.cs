using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WaterCompany.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration; 

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Service()
        {
            return View();
        }

        public IActionResult WhyUs()
        {
            return View();
        }

        public IActionResult Team()
        {
            return View();
        }

        public IActionResult Location()
        {
            var googleMapsApiKey = _configuration["GoogleMaps:ApiKey"];
            ViewBag.GoogleMapsApiKey = googleMapsApiKey;

            return View();
        }
    }
}
