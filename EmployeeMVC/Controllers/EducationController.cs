using EmployeeMVC.Contexts;
using EmployeeMVC.Models;
using EmployeeMVC.Repositories;
using EmployeeMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Controllers;

public class EducationController : Controller
{
    private readonly MyContext context;
    public EducationController(MyContext context)
    {
        this.context = context;
    }
    public IActionResult Index()
    {
        var results = 
    }
    public IActionResult Details(int id)
    {
        var educations = context.Educations.Find(id);
        return View(new EducationUniversityVM
        {
            Id = educations.Id,
            Degree = educations.Degree,
            Gpa = educations.Gpa,
            Major = educations.Major,
            UniversityName = context.Universities.Find(educations.UniversityId).Name,
        });
    }
    public IActionResult Create()
    {
        var universities = context.Universities.ToList()
            .Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
        ViewBag.UniversityId = universities;
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EducationUniversityVM education)
    {
        context.Add(new Education
        {
            Id = education.Id,
            Degree = education.Degree,
            Gpa = education.Gpa,
            Major = education.Major,
            UniversityId = Convert.ToInt16(education.UniversityName)
        });
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }
    public IActionResult Edit(int id)
    {
        var education = context.Educations.Find(id);
        var universities = context.Universities.ToList()
            .Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
        ViewBag.UniversityId = universities;
        return View(education);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Education education)
    {
        context.Entry(education).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    public IActionResult Delete(int id)
    {
        var education = context.Educations.Find(id);
        return View(education);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var education = context.Educations.Find(id);
        context.Remove(education);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}
