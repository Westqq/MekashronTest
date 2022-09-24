using IICUTechReference;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestNetFromHome.Models;
using TestNetFromHome.ViewModels;

namespace TestNetFromHome.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Auth(AuthViewModel model)
        {
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
                return View("Index", new AuthErrorModel {  ErrorMessage  = "Некоректно введены данные!" });

            var techClient = new ICUTechClient();
            var authResult = techClient.Login(model.UserName, model.Password, string.Empty);

            var user = JsonConvert.DeserializeObject<CurrentUser>(authResult);

            if (user == null || !string.IsNullOrEmpty(user.ErrorMessage))
            {
                return View("Index", new AuthErrorModel { ErrorMessage = user?.ErrorMessage ?? "Ошибка запроса" });
            }

            return PartialView("AuthSuccess", new AuthSuccessModel { Email = user.Email, FirstName = user.FirstName, LastName = user.LastName });

        }
    }
}