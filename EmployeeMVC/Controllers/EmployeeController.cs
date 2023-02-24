using EmployeeMVC.Contexts;
using EmployeeMVC.Models;
using EmployeeMVC.Repositories;
using EmployeeMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace EmployeeMVC.Controllers;

public class EmployeeController : Controller
{
    private readonly MyContext context;
    private readonly EmployeeRepository employeeRepository;
    public EmployeeController(MyContext context, EmployeeRepository employeeRepository)
    {
        this.context = context;
        this.employeeRepository = employeeRepository;
    }
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var employee = employeeRepository.GetAll()
            .Select(e => new EmployeeVM
            {
                NIK = e.NIK,
                Email = e.Email,
                Birthdate = e.Birthdate,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Gender = e.Gender,
                HiringDate = e.HiringDate,
                PhoneNumber = e.PhoneNumber
            }).ToList();
        return View(employee);
    }

    //Get
    public IActionResult Details(string NIK)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var employee = employeeRepository.GetById(NIK);
        return View(new EmployeeVM
        {
            NIK = employee.NIK,
            Email = employee.Email,
            Birthdate = employee.Birthdate,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Gender = employee.Gender,
            HiringDate = employee.HiringDate,
            PhoneNumber = employee.PhoneNumber
        });
    }

    //Get
    public IActionResult Create()
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EmployeeVM employee)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var result = employeeRepository.Insert(new Employee
        {
            NIK = employee.NIK,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Birthdate = employee.Birthdate,
            Gender = (Models.GenderEnum)employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber
        });
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    //Get
    public IActionResult Edit(string NIK)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var employee = context.Employees.Find(NIK);
        return View(new EmployeeVM
        {
            NIK = employee.NIK,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Birthdate = employee.Birthdate,
            Gender = employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EmployeeVM employee)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var result = employeeRepository.Update(new Employee
        {
            NIK = employee.NIK,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Birthdate = employee.Birthdate,
            Gender = employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber
        });
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    public IActionResult Delete(string NIK)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var employee = employeeRepository.GetById(NIK);
        return View(new EmployeeVM
        {
            NIK = employee.NIK,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Birthdate = employee.Birthdate,
            Gender = employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(string NIK)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var result = employeeRepository.Delete(NIK);
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}