using AdminLTE2.Data;
using AdminLTE2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminLTE2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var orders = _context.orders.Include(o => o.Customers).Include(o => o.Employees).ToList();
            return View(orders);
        }

        public IActionResult Create()
        {
            ViewData["customer_id"] = new SelectList(_context.customers, "customer_id", "name");
            ViewData["salesman_id"] = new SelectList(_context.employees, "employee_id", "first_name");
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = _context.orders.Find(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["customer_id"] = new SelectList(_context.customers, "customer_id", "name", orders.customer_id);
            ViewData["salesman_id"] = new SelectList(_context.employees, "employee_id", "first_name", orders.salesman_id);
            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Orders orders)
        {

            if (ModelState.IsValid)
            {
                //_context.orders.AddAsync(orders);
                _context.orders.Add(orders);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "La Orden se guardo correctamente";
                return RedirectToAction("Index");
            }
            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Orders orders)
        {
            if (id != orders.order_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(orders);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "La Orden se actualizo correctamente";
                return RedirectToAction("Index");
            }
            return View(orders);


        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = _context.orders.Where(o => o.order_id == id).FirstOrDefault();

            _context.orders.Remove(orders);
            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
