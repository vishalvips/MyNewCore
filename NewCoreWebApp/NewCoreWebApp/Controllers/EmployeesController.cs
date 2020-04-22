using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using DataLayer.Entities;
using NewCoreWebApp.Models;

namespace NewCoreWebApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DataContext _context;

        public EmployeesController(DataContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Employees.Include(e => e.department).Select(e => new EmployeeModel {
                Name = e.Name,
                Email=e.Email,
                Dob=e.Dob,
                State=e.State,
                Address=e.Address,
                City=e.City,
                Id=e.Id,
                Zip=e.Zip,
                Phone=e.Phone,
                JoiningDate=e.JoiningDate,
                DepartmentName=e.department.DepartmentName
            }).ToListAsync();
            
            return View(await dataContext);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["deptId"] = new SelectList(_context.Departments, "DepartmentName", "DepartmentName");
            return View();
        }

        // POST: Employees/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Dob,Phone,Email,Address,State,City,Zip,JoiningDate,DepartmentName")] EmployeeModel employee)
        {
            int getId = _context.Departments.Where(e => e.DepartmentName == employee.DepartmentName).Select(id=>id.DepartmentId).First();
            if (employee != null)
            {
                Employee emp = new Employee();
                ViewData["deptId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", emp.deptId);
                emp.Name = employee.Name;
                emp.Dob = employee.Dob;
                emp.Email = employee.Email;
                emp.Address = employee.Address;
                emp.Phone = employee.Phone;
                emp.deptId = getId;
                emp.State = employee.State;
                emp.City = employee.City;
                emp.Zip = employee.Zip;
                emp.JoiningDate = employee.JoiningDate;
                _context.Add(emp);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index)); 
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            var editEmployee = new EmployeeModel();
            editEmployee.Id = employee.Id;
            editEmployee.Name = employee.Name;
            editEmployee.Dob = employee.Dob;
            editEmployee.Email = employee.Email;
            editEmployee.Phone = employee.Phone;
            editEmployee.Address = employee.Address;
            editEmployee.DepartmentId = employee.deptId;
            editEmployee.State = employee.State;
            editEmployee.City = employee.City;
            editEmployee.Zip = employee.Zip;
            editEmployee.JoiningDate = employee.JoiningDate;

            if (editEmployee == null)
            {
                return NotFound();
            }
            ViewData["deptId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", employee.deptId);
            return View(editEmployee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Dob,Phone,Email,Address,State,City,Zip,JoiningDate,DepartmentId")] EmployeeModel employee)
        {
            var emp = _context.Employees.First(a => a.Id == id);
            if (id != emp.Id)
            {
                return NotFound();
            }
            if(employee != null)
            {
                emp.Name = employee.Name;
                emp.Dob = employee.Dob;
                emp.Phone = employee.Phone;
                emp.Email = employee.Email;
                emp.Address = employee.Address;
                emp.State = employee.State;
                emp.City = employee.City;
                emp.Zip = employee.Zip;
                emp.JoiningDate = employee.JoiningDate;
                emp.deptId = employee.DepartmentId;
                    await _context.SaveChangesAsync();
                
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!EmployeeExists(employee.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.department)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
