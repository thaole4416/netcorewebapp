﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index(long? id)
        {
            ViewBag.Categories = new SelectList(context.Categories, "CategoryId", "Name");
            return View("Form",
                await context.Products.Include(p => p.Category).Include(p => p.Supplier)
                    .FirstOrDefaultAsync(p => id == null || p.ProductId == id));
        }

        // [HttpPost]
        // public IActionResult SubmitForm(Category category)
        // {
        //     TempData["category"] = System.Text.Json.JsonSerializer.Serialize(category);
        //     return RedirectToAction(nameof(Results));
        // }

        [HttpPost]
        public IActionResult SubmitForm([Bind(Prefix = "Category")] Category category)
        {
            TempData["category"] = System.Text.Json.JsonSerializer.Serialize(category);
            return RedirectToAction(nameof(Results));
        }

        public IActionResult Results()
        {
            return View((TempDataDictionary) TempData);
        }
    }
}