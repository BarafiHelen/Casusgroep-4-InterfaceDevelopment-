using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class SearchController : Controller
    {
        private readonly MatrixIncDbContext _context;

        public SearchController(MatrixIncDbContext context)
        {
            _context = context;
        }

        // /Search/Global
        public async Task<IActionResult> Global(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return Json(new List<string>());
            }

            var productResults = await _context.Products
                .Where(p => p.IsActive && p.Name.Contains(term))
                .Select(p => new
                {
                    type = "Product",
                    name = p.Name,
                    url = Url.Action("Details", "Products", new { id = p.Id })
                })
                .ToListAsync();

            var customerResults = await _context.Customers
                .Where(c => c.IsActive && c.Name.Contains(term))
                .Select(c => new
                {
                    type = "Customer",
                    name = c.Name,
                    url = Url.Action("Details", "Customers", new { id = c.Id })
                })
                .ToListAsync();

            var combined = productResults.Concat(customerResults);
            return Json(combined);
        }
    }
}
