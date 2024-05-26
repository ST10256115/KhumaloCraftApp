using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KhumaloCraft.Data;
using KhumaloCraft.Models;
using KhumaloCraft.ViewModels;

namespace KhumaloCraft.Controllers
{
    public class CustomersController : Controller
    {
        private readonly KhumaloCraftAppContext _context;

        public CustomersController(KhumaloCraftAppContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,PhoneNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,PhoneNumber")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction("SignUpSuccess");
            }
            return View(customer);
        }

        public IActionResult SignUpSuccess()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string phoneNumber)
        {
      
            return RedirectToAction("PlaceOrder", new { phoneNumber });
        }
        [HttpGet]
        public IActionResult CustomerLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CustomerLogin(CustomerLoginViewModel model)
        {
            var customer = _context.Customers
                .FirstOrDefault(c => c.Email == model.Email && c.PhoneNumber == model.PhoneNumber);

            if (customer != null)
            {
                return RedirectToAction("OrderEntry", new { customerId = customer.CustomerID });
            }

            ModelState.AddModelError(string.Empty, "Invalid login credentials.");
            return View(model);
        }

        [HttpGet]
        public IActionResult OrderEntry(int customerId)
        {
            ViewBag.CustomerId = customerId;
            return View();
        }

        [HttpPost]
        public IActionResult OrderEntry(int customerId, string productName)
        {
            var product = _context.Products.FirstOrDefault(p => p.Name == productName && p.Availability);

            if (product != null)
            {
                var order = new Order
                {
                    CustomerID = customerId,
                    OrderDate = DateTime.Now,
                    TotalAmount = product.Price,
                    Status = "Pending" 
                };
                _context.Orders.Add(order);
                _context.SaveChanges();

                var orderItem = new OrderItem
                {
                    OrderID = order.OrderID,
                    ProductID = product.ProductID,
                    UnitPrice = product.Price,
                    Quantity = 1
                };
                _context.OrderItems.Add(orderItem);
                _context.SaveChanges();

                product.Availability = false;
                _context.Products.Update(product);
                _context.SaveChanges();

                ViewBag.Message = "Order placed successfully!";
            }
            else
            {
                ViewBag.Message = "Product not available.";
            }

            ViewBag.CustomerId = customerId;
            return View();
        }
        public IActionResult CustomerLoginForOrders()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CustomerLoginForOrders(CustomerLoginViewModel model)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == model.Email && c.PhoneNumber == model.PhoneNumber);

            if (customer != null)
            {
                return RedirectToAction("ViewOrders", new { customerId = customer.CustomerID });
            }

            ModelState.AddModelError(string.Empty, "Invalid login credentials.");
            return View(model);
        }

        [HttpGet]
        public IActionResult ViewOrders(int customerId)
        {
            var orders = _context.Orders
                .Where(o => o.CustomerID == customerId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Select(o => new OrderDetailsViewModel
                {
                    OrderID = o.OrderID,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemViewModel
                    {
                        ProductName = oi.Product.Name,
                        UnitPrice = oi.UnitPrice,
                        Quantity = oi.Quantity
                    }).ToList()
                }).ToList();

            return View(orders);
        }

    }

}

