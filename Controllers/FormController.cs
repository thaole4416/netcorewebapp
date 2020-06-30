using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class FormController : Controller
    {
        private DataContext context;

        public FormController(DataContext dbContext)
        {
            context = dbContext;
        }

        public async Task<IActionResult> Index(long id = 1)
        {
            Category defaultSelect = new Category() {CategoryId = 0, Name = "Default Selector"};
            
            ViewBag.Categories= new SelectList(context.Categories.AsEnumerable().Prepend(defaultSelect), "CategoryId", "Name");
            return View("Form", await context.Products.FindAsync(id));
        }

        [HttpPost]
        public IActionResult SubmitForm()
        {
            foreach (string key in Request.Form.Keys)
            {
                TempData[key] = string.Join(", ", Request.Form[key]);
            }

            return RedirectToAction(nameof(Results));
        }

        public IActionResult Results()
        {
            return View(TempData);
        }
    }
}