using EmployeeMVC.Contexts;
using EmployeeMVC.Models;
using EmployeeMVC.Repositories;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    public IActionResult Index()
    {
        //if (HttpContext.Session.GetString("email") == null)
        //{
        //    return RedirectToAction("Unauthorized", "Error");
        //}
        var universities = repository.GetAll();
        return View(universities);
    }
    [Authorize]
    public IActionResult Details(int id)
    {
        //if (HttpContext.Session.GetString("email") == null)
        //{
        //    return RedirectToAction("Unauthorized", "Error");
        //}
        var university = repository.GetById(id);
        return View(university);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        //if (HttpContext.Session.GetString("email") == null)
        //{
        //    return RedirectToAction("Unauthorized", "Error");
        //}
        //if (HttpContext.Session.GetString("role") != "Admin")
        //{
        //    return RedirectToAction("Forbidden", "Error");
        //}
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(University university)
    {
        //if (HttpContext.Session.GetString("email") == null)
        //{
        //    return RedirectToAction("Unauthorized", "Error");
        //}
        //if (HttpContext.Session.GetString("role") != "Admin")
        //{
        //    return RedirectToAction("Forbidden", "Error");
        //}
        var result = repository.Insert(university);
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        //if (HttpContext.Session.GetString("email") == null)
        //{
        //    return RedirectToAction("Unauthorized", "Error");
        //}
        //if (HttpContext.Session.GetString("role") != "Admin")
        //{
        //    return RedirectToAction("Forbidden", "Error");
        //}
        var university = repository.GetById(id);
        return View(university);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(University university)
    {
        //if (HttpContext.Session.GetString("email") == null)
        //{
        //    return RedirectToAction("Unauthorized", "Error");
        //}
        //if (HttpContext.Session.GetString("role") != "Admin")
        //{
        //    return RedirectToAction("Forbidden", "Error");
        //}
        var result = repository.Update(university);
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        //if (HttpContext.Session.GetString("email") == null)
        //{
        //    return RedirectToAction("Unauthorized", "Error");
        //}
        //if (HttpContext.Session.GetString("role") != "Admin")
        //{
        //    return RedirectToAction("Forbidden", "Error");
        //}
        var university = repository.GetById(id);
        return View(university);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        //if (HttpContext.Session.GetString("email") == null)
        //{
        //    return RedirectToAction("Unauthorized", "Error");
        //}
        //if (HttpContext.Session.GetString("role") != "Admin")
        //{
        //    return RedirectToAction("Forbidden", "Error");
        //}
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
