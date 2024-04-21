using AdminLTE2.Data;
using AdminLTE2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace AdminLTE2.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var contacts = _context.contacts.Include(c => c.Customers).ToList();
            return View(contacts);
        }

        public IActionResult Create()
        {
            ViewData["customer_id"] = new SelectList(_context.customers, "customer_id", "name");
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacts = _context.contacts.Find(id);
            if (contacts == null)
            {
                return NotFound();
            }
            ViewData["customer_id"] = new SelectList(_context.customers, "customer_id", "name", contacts.customer_id);
            return View(contacts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contacts contacts)
        {
           
            if (ModelState.IsValid)
            {
                _context.contacts.AddAsync(contacts);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Contacto se guardo correctamente";
                return RedirectToAction("Index");
            }
            //ViewData["customer_id"] = new SelectList(_context.customers, "customer_id", "name", contacts.customer_id);
            return View(contacts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Contacts contacts)
        {
            if (id != contacts.contact_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(contacts);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Contacto se actualizo correctamente";
                return RedirectToAction("Index");
            }
            return View(contacts);


        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _context.contacts.Remove(_context.contacts.Find(id));
            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
