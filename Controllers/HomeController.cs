using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebApp.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace WebApp.Controllers
{
    [Message("This is the controller-scoped filter")]
    public class HomeController : Controller
    {
        [Message("This is the first action-scoped filter")]
        [Message("This is the second action-scoped filter")]
        public IActionResult Index()
        {
            return View("Message", "This is the Index action on the Home controller");
        }
    }
}