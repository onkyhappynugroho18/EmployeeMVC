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
        var employee = employeeRepository.GetEmployee();
        return View(employee);
    }

    //Get
    public IActionResult Details(string NIK)
    {
        var employee = employeeRepository.GetEmployeeById(NIK);
        return View(employee);
    }

    //Get
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EmployeeVM employee)
    {
        context.Add(new Employee
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
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    //Get
    public IActionResult Edit(string NIK)
    {
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
        context.Entry(new Employee
        {
            NIK = employee.NIK,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Birthdate = employee.Birthdate,
            Gender = employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber
        }).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    public IActionResult Delete(string NIK)
    {
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
    public IActionResult Remove(string NIK)
    {
        var employee = context.Employees.Find(NIK);
        context.Remove(employee);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}