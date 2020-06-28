using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private DataContext context;

        public SuppliersController(DataContext ctx)
        {
            context = ctx;
        }

        [HttpGet("{id}")]
        public async Task<Supplier> GetSupplier(long id)
        {
            var supplier = await context.Suppliers.Include(s => s.Products).FirstAsync(s => s.SupplierId == id);
            foreach (var product in supplier.Products)
            {
                product.Supplier = null;
            }

            return supplier;
        }


        [HttpPut("{id}")]
        public async Task<Supplier> PatchSupplier(long id, JsonPatchDocument<Supplier> patchDoc)
        {
            Supplier s = await context.Suppliers.FindAsync(id);
            if (s != null)
            {
                patchDoc.ApplyTo(s);
                context.SaveChangesAsync();
            }
            return s;
        }
    }
}