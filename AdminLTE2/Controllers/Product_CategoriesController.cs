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

        public Product_CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var product_categories = _context.product_categories.ToList();
            return View(product_categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_categories = _context.product_categories.Find(id);
            if (product_categories == null)
            {
                return NotFound();
            }
            return View(product_categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product_Categories product_Categories)
        // public IActionResult Create([Bind("category_id,category_name")]Product_Categories product_categories)
        {

            if (ModelState.IsValid) 
            {
                _context.product_categories.AddAsync(product_Categories);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "La categoria se guardo correctamente";
                return RedirectToAction(nameof(Index));
            }
            
            return View(product_Categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product_Categories product_categories)
        {
            // if (id != product_categories.category_id)
            // {
            //     return NotFound();
            // }

            if (ModelState.IsValid)
            {
                _context.Update(product_categories);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "La categoria se actualizo correctamente";
                return RedirectToAction(nameof(Index));
            }
            return View(product_categories);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product_Categories = _context.product_categories.Where(c => c.category_id == id).FirstOrDefault();
            _context.product_categories.Remove(product_Categories);
            _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
