using AdminLTE2.Data;
using AdminLTE2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System.Net;

namespace AdminLTE2.Controllers
{
    public class Product_CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int id;

        public Product_CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //var product_categories = _context.product_categories.Include(l => l.category_id).ToList();
            var product_categories = _context.product_categories.AsQueryable();
            return View(product_categories);
        }
        public IActionResult Create()
        {
            ViewData["category:id"] = new SelectList(_context.product_categories, " category_id, category_name");

            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_categories = _context.product_categories.Find(id);
            //var product_categories = _context.product_categories.Where(c => c.category_id == id).FirstOrDefault();
            if (product_categories == null)
            {
                return NotFound();
            }
            return View(product_categories);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Create(Locations locations)
        //public IActionResult Create(Locations locations)
        public IActionResult Create([Bind("category_id,category_name")]Product_Categories product_categories)
        {

            if (ModelState.IsValid) 
            {
                _context.product_categories.Add(product_categories);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Contacto se guardo correctamente";
                return RedirectToAction("Index");
            }
            //ViewData["customer_id"] = new SelectList(_context.customers, "customer_id", "name", contacts.customer_id);
            return View(product_categories);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product_Categories product_categories)
        {
            if (id != product_categories.category_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(product_categories);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Contacto se actualizo correctamente";
                return RedirectToAction("Index");
            }
            return View(product_categories);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var locations = _context.product_categories.Where(c => c.category_id == id).FirstOrDefault();
            //_context.product_categories.Remove(_context.product_categories.Find(id));
            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }






    }
}
