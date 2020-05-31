using System.Collections.Generic;
using System.Linq;
using HelpDeskIgnatov.Data;
using HelpDeskIgnatov.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HelpDeskIgnatov.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = _context.Users.Include(u => u.Department).Include(u => u.Role).ToList();

            List<Department> departments = _context.Departments.ToList();
            
            departments.Insert(0, new Department { Name = "Все", Id = 0 });
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            List<Role> roles = _context.Roles.ToList();
            roles.Insert(0, new Role { Name = "Все", Id = 0 });
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            return View(users);
        }

        [HttpPost]
        public ActionResult Index(int department, int role)
        {
            IEnumerable<User> allUsers = null;
            if (role == 0 && department == 0)
            {
                return RedirectToAction("Index");
            }
            if (role == 0 && department != 0)
            {
                allUsers = _context.Users.Include(u => u.Department)
                    .Include(u => u.Role)
                    .Where(user => user.DepartmentId == department);
            }
            else if (role != 0 && department == 0)
            {
                allUsers = _context.Users.Include(u => u.Department)
                    .Include(u => u.Role)
                    .Where(user => user.RoleId == role);
            }
            else
            {
                allUsers = _context.Users.Include(u => u.Department)
                    .Include(u => u.Role)
                    .Where(user => user.DepartmentId == department && user.RoleId == role);
            }

            List<Department> departments = _context.Departments.ToList();
            
            departments.Insert(0, new Department { Name = "Все", Id = 0 });
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            List<Role> roles = _context.Roles.ToList();
            roles.Insert(0, new Role { Name = "Все", Id = 0 });
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            return View(allUsers.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult Create()
        {
            SelectList departments = new SelectList(_context.Departments, "Id", "Name");
            ViewBag.Departments = departments;
            SelectList roles = new SelectList(_context.Roles, "Id", "Name");
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            SelectList departments = new SelectList(_context.Departments, "Id", "Name");
            ViewBag.Departments = departments;
            SelectList roles = new SelectList(_context.Roles, "Id", "Name");
            ViewBag.Roles = roles;

            return View(user);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult Edit(int id)
        {
            User user = _context.Users.Find(id);
            SelectList departments = new SelectList(_context.Departments, "Id", "Name", user.DepartmentId);
            ViewBag.Departments = departments;
            SelectList roles = new SelectList(_context.Roles, "Id", "Name", user.RoleId);
            ViewBag.Roles = roles;

            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            SelectList departments = new SelectList(_context.Departments, "Id", "Name");
            ViewBag.Departments = departments;
            SelectList roles = new SelectList(_context.Roles, "Id", "Name");
            ViewBag.Roles = roles;

            return View(user);
        }
        [Authorize(Roles = "Администратор")]
        public ActionResult Delete(int id)
        {
            User user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
