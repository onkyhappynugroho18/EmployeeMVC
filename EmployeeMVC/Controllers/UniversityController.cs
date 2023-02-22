using EmployeeMVC.Contexts;
using EmployeeMVC.Models;
using EmployeeMVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace EmployeeMVC.Controllers;

public class UniversityController : Controller
{
    private readonly UniversityRepository repository;
    public UniversityController(UniversityRepository repository)
    {
        this.repository = repository;
    }
    public IActionResult Index()
    {
        var universities = repository.GetAll();
        return View(universities);
    }
    public IActionResult Details(int id)
    {
        var university = repository.GetById(id);
        return View(university);
    }
    public IActionResult Create()
    {
        //var universities = context.Universities.ToList()
        //    .Select(u => new SelectListItem
        //    {
        //        Value = u.Id.ToString(),
        //        Text = u.Name
        //    });
        //context.Universities.ToList();
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(University university)
    {
        var result = repository.Insert(university);
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }
    public IActionResult Edit(int id)
    {
        var university = repository.GetById(id);
        return View(university);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(University university)
    {
        var result = repository.Update(university);
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    public IActionResult Delete(int id)
    {
        var university = repository.GetById(id);
        return View(university);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var result = repository.Delete(id);
        if (result == 0)
        {
            // Data Tidak Ditemukan
        }
        else
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}
