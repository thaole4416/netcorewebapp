﻿using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private DataContext context;

        public ProductsController(DataContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IAsyncEnumerable<Product> GetProducts()
        {
            return context.Products;
        }

        [HttpGet("{id}")]
        public async Task<Product> GetProduct(long id)
        {
            return await context.Products.FindAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct( ProductBindingTarget target)
        {
            if (ModelState.IsValid)
            {
                Product p = target.ToProduct();
                await context.Products.AddAsync(p);
                await context.SaveChangesAsync();
                return Ok(p);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task UpdateProduct( Product product)
        {
            context.Update(product);
            await context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteProduct(long id)
        {
            context.Products.Remove(new Product() {ProductId = id});
            await context.SaveChangesAsync();
        }
    }
}