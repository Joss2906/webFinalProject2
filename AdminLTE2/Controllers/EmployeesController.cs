using AdminLTE2.Data;
using AdminLTE2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminLTE2.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var employees = _context.employees.Include(e => e.Managers).ToList();
            return View(employees);
        }

        public IActionResult Create()
        {
            var managers = _context.employees.Select(e => new {
                e.employee_id,
                FullName = $"{e.first_name} {e.last_name}"
            }).ToList();

            ViewData["manager_id"] = new SelectList(managers, "employee_id", "FullName");
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = _context.employees.Find(id);
            if (employees == null)
            {
                return NotFound();
            }

            var managers = _context.employees.Select(e => new {
                e.employee_id,
                FullName = $"{e.first_name} {e.last_name}"
            }).ToList();

            ViewData["manager_id"] = new SelectList(managers, "employee_id", "FullName", employees.manager_id);
            return View(employees);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employees employees)
        {
            
            if (ModelState.IsValid)
            {
                _context.employees.AddAsync(employees);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Empleado se guardo correctamente";
                return RedirectToAction("Index");
            }
            return View(employees);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employees employees)
        {
            if (id != employees.employee_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(employees);
                _context.SaveChangesAsync();

                TempData["mensaje"] = "El Empleado se actualizo correctamente";
                return RedirectToAction("Index");
            }
            return View(employees);


        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = _context.employees.Where(e => e.employee_id == id).FirstOrDefault();
            _context.employees.Remove(employees);
            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
