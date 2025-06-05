using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Interfaces;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class OrdersController : Controller
    {
        private readonly MatrixIncDbContext _context;
        private readonly IProductRepository _productRepo;
        private readonly IOrderItemRepository _orderItemRepo;
        private readonly ICustomerRepository _customerRepo;

        public OrdersController(MatrixIncDbContext context, IProductRepository productRepo, IOrderItemRepository orderItemRepo, ICustomerRepository customerRepo)
        {
            _context = context;
            _productRepo = productRepo;
            _orderItemRepo = orderItemRepo;
            _customerRepo = customerRepo;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = _context.Orders.Include(o => o.Customer);
            return View(await orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewBag.CustomerList = new SelectList(_context.Customers.Where(c => c.IsActive), "Id", "Name");
            ViewBag.ProductList = _productRepo.GetAllProducts().ToList();
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order, List<int> productIds, List<int> quantities)
        {
            if (productIds.Count != quantities.Count)
            {
                ViewBag.CustomerList = new SelectList(_context.Customers.Where(c => c.IsActive), "Id", "Name", order.CustomerId);
                ViewBag.ProductList = _productRepo.GetAllProducts();
                ModelState.AddModelError("", "Mismatch in product and quantity counts.");
                return View(order);
            }

            order.OrderDate = DateTime.Now;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); // Get order ID

            for (int i = 0; i < productIds.Count; i++)
            {
                if (quantities[i] > 0)
                {
                    var item = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = productIds[i],
                        Quantity = quantities[i]
                    };
                    _orderItemRepo.Add(item);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            ViewBag.CustomerList = new SelectList(_customerRepo.GetAllCustomers(), "Id", "Name", order.CustomerId);
            return View(order);
        }
        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CustomerList = new SelectList(_customerRepo.GetAllCustomers(), "Id", "Name", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
