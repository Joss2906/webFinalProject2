using AdminLTE2.Data;
using AdminLTE2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminLTE2.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var products = _context.products.Include(p => p.Product_Categories).ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            ViewData["category_id"] = new SelectList(_context.product_categories, "category_id", "category_name");
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = _context.products.Find(id);
            if (products == null)
            {
                return NotFound();
            }

            ViewData["category_id"] = new SelectList(_context.product_categories, "category_id", "category_name", products.category_id);
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Products products)
        {
            if (ModelState.IsValid)
            {
                _context.products.AddAsync(products);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Producto se guardo correctamente";
                return RedirectToAction("Index");
            }
            //ViewData["customer_id"] = new SelectList(_context.customers, "customer_id", "name", products.customer_id);
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Products products)
        {
            if (id != products.product_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(products);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Producto se actualizo correctamente";
                return RedirectToAction("Index");
            }
            return View(products);


        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _context.products.Remove(_context.products.Find(id));
            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
