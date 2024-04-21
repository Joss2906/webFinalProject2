using AdminLTE2.Data;
using AdminLTE2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminLTE2.Controllers
{
    public class Order_ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public Order_ItemsController (ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var order_items = _context.order_items.Include(i => i.Orders).Include(i => i.Products).ToList();
            return View(order_items);
        }

        public IActionResult Create()
        {
            ViewData["product_id"] = new SelectList(_context.products, "product_id", "product_name");
            ViewData["order_id"] = new SelectList(_context.orders, "order_id", "order_id");
            return View();
        }

        public IActionResult Edit(int? i_id, int? o_id)
        {
            if (i_id == null)
            {
                return NotFound();
            }

            var order_items = _context.order_items
                .Where(i => i.item_id == i_id)
                .Where(i => i.order_id == o_id)
                .FirstOrDefault();

            if (order_items == null)
            {
                return NotFound();
            }

            ViewData["product_id"] = new SelectList(_context.products, "product_id", "product_name");
            ViewData["order_id"] = new SelectList(_context.orders, "order_id", "order_id", order_items.order_id);
            return View(order_items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order_Items order_items)
        {

            if (ModelState.IsValid)
            {
                _context.order_items.AddAsync(order_items);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El item se guardo correctamente";
                return RedirectToAction("Index");
            }

            return View(order_items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Order_Items order_items)
        {
            //print order_items
            // Console.WriteLine(order_items.ToString);
            //if (id != order_items.product_id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                _context.Update(order_items);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El item se actualizo correctamente";
                return RedirectToAction("Index");
            }
            //return View(order_items);
            return View();


        }

        public IActionResult Delete(int i_id, int o_id)
        {
            
            var order_items = _context.order_items
                .Where(i => i.item_id == i_id)
                .Where(i => i.order_id == o_id)
                .FirstOrDefault();

            _context.order_items.Remove(order_items);
            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
