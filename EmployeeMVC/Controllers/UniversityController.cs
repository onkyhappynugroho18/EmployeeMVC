using EmployeeMVC.Contexts;
using EmployeeMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Controllers;

public class UniversityController : Controller
{
    private readonly MyContext context;
    public UniversityController(MyContext context)
    {
        this.context = context;
    }
    public IActionResult Index()
    {
        var universities = context.Universities.ToList();
        return View(universities);
    }
    public IActionResult Details(int id)
    {
        var university = context.Universities.Find(id);
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
        context.Add(university);
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }
    public IActionResult Edit(int id)
    {
        var university = context.Universities.Find(id);
        return View(university);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(University university)
    {
        context.Entry(university).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    public IActionResult Delete(int id)
    {
        var university = context.Universities.Find(id);
        return View(university);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var university = context.Universities.Find(id);
        context.Remove(university);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}
