using EmployeeMVC.Contexts;
using EmployeeMVC.Models;
using EmployeeMVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Controllers;

public class AccountController : Controller
{
    private readonly MyContext context;
    public AccountController(MyContext context)
    {
        this.context = context;
    }
    public IActionResult Index()
    {
        //var results = context.Employees.Join(
        //    context.Accounts,
        //    e => e.NIK,
        //    a => a.EmployeeNIK,
        //    (e, a) => new LoginVM
        //    {
        //        Email = e.Email,
        //        Password = a.Password
        //    });
        var account = context.Accounts.ToList();
        return View(account);
    }
    public IActionResult Details(string NIK)
    {
        var account = context.Accounts.Find(NIK);
        return View(account);
    }
    public IActionResult Create()
    {
        context.Accounts.ToList();
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Account account)
    {
        context.Add(account);
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }
    public IActionResult Edit(string NIK)
    {
        var account = context.Accounts.Find(NIK);
        return View(account);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Account account)
    {
        context.Entry(account).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    public IActionResult Delete(string NIK)
    {
        var account = context.Accounts.Find(NIK);
        return View(account);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(string NIK)
    {
        var account = context.Accounts.Find(NIK);
        context.Remove(account);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Register()
    {
        //var gender = new List<SelectListItem>
        //{ new SelectListItem
        //{
        //    Value = "0",
        //    Text = "Male"
        //},
        //new SelectListItem
        //{
        //    Value = "1",
        //    Text = "Female"
        //},
        //};
        //ViewBag.Gender = gender;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterVM registerVM)
    {
        if (ModelState.IsValid)
        {
            University university = new University
            {
                Name = registerVM.UniversityName
            };

            //If untuk university agar tidak duplikat
            if (context.Universities.Any(u => u.Name == university.Name))
            {
                university.Id = context.Universities
                    .FirstOrDefault(u => u.Name == university.Name).Id;
            }
            else
            {
                context.Universities.Add(university);
                context.SaveChanges();
            }

            Education education = new Education
            {
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                Gpa = registerVM.GPA,
                UniversityId = university.Id
            };
            context.Educations.Add(education);
            context.SaveChanges();

            Employee employee = new Employee
            {
                NIK = registerVM.NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Birthdate = registerVM.Birthdate,
                Gender = registerVM.Gender,
                HiringDate = registerVM.HiringDate,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
            };
            context.Employees.Add(employee);
            context.SaveChanges();

            Account account = new Account
            {
                EmployeeNIK = registerVM.NIK,
                Password = registerVM.Password
            };
            context.Accounts.Add(account);
            context.SaveChanges();

            AccountRole accountRole = new AccountRole
            {
                AccountNIK = registerVM.NIK,
                RoleId = 1
            };

            context.AccountRoles.Add(accountRole);
            context.SaveChanges();

            Profiling profiling = new Profiling
            {
                EmployeeNIK = registerVM.NIK,
                EducationId = education.Id
            };
            context.Profilings.Add(profiling);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    public IActionResult Login()
    {
        var results = context.Employees.Join(
            context.Accounts,
            e => e.NIK,
            a => a.EmployeeNIK,
            (e, a) => new LoginVM
            {
                Email = e.Email,
                Password = a.Password
            });
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginVM loginVM)
    {
        var results = context.Employees.Join(
            context.Accounts,
            e => e.NIK,
            a => a.EmployeeNIK,
            (e, a) => new LoginVM
            {
                Email = e.Email,
                Password = a.Password
            });
        if (results.Any(e => e.Email == loginVM.Email && e.Password == loginVM.Password))
        {
            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError(string.Empty, "Account atau Password Tidak Ditemukan!");
        return View();
    }
}
