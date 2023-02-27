using EmployeeMVC.Contexts;
using EmployeeMVC.Models;
using EmployeeMVC.Repositories;
using EmployeeMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Controllers;

[Authorize]
public class EducationController : Controller
{
    private readonly MyContext context;
    private readonly EducationRepository repository;
    private readonly UniversityRepository universityRepository;

    public EducationController(MyContext context, EducationRepository repository, UniversityRepository universityRepository)
    {
        this.context = context;
        this.repository = repository;
        this.universityRepository = universityRepository;
    }

    [Authorize]
    public IActionResult Index()
    {
        //if (HttpContext.Session.GetString("email") == null)
        //{
        //    return RedirectToAction("Unauthorized", "Error");
        //}
        var results = repository.GetEducationUniversities();
        return View(results);
    }

    [Authorize]
    public IActionResult Details(int id)
    {
        //if (HttpContext.Session.GetString("email") == null)
        //{
        //    return RedirectToAction("Unauthorized", "Error");
        //}
        var educations = repository.GetEduUnivById(id);
        return View(educations);
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
        var universities = universityRepository.GetAll()
            .Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
        ViewBag.UniversityId = universities;
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EducationUniversityVM education)
    {
        //if (HttpContext.Session.GetString("email") == null)
        //{
        //    return RedirectToAction("Unauthorized", "Error");
        //}
        //if (HttpContext.Session.GetString("role") != "Admin")
        //{
        //    return RedirectToAction("Forbidden", "Error");
        //}
        var result = repository.Insert(new Education
        {
            Id = education.Id,
            Degree = education.Degree,
            Gpa = education.Gpa,
            Major = education.Major,
            UniversityId = Convert.ToInt16(education.UniversityName)
        });
        if (result > 0)
            return RedirectToAction(nameof(Index));
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
        var universities = universityRepository.GetAll()
            .Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
        ViewBag.UniversityId = universities;

        var education = repository.GetById(id);
        return View(new EducationUniversityVM
        {
            Id = id,
            Degree = education.Degree,
            Gpa = education.Gpa,
            Major = education.Major,
            UniversityName = context.Universities.Find(education.UniversityId).Name
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EducationUniversityVM education)
    {
        //if (HttpContext.Session.GetString("email") == null)
        //{
        //    return RedirectToAction("Unauthorized", "Error");
        //}
        //if (HttpContext.Session.GetString("role") != "Admin")
        //{
        //    return RedirectToAction("Forbidden", "Error");
        //}
        var result = repository.Update(new Education
        {
            Id = education.Id,
            Degree = education.Degree,
            Gpa = education.Gpa,
            Major = education.Major,
            UniversityId = Convert.ToInt16(education.UniversityName)
        });
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
        var education = repository.GetById(id);
        return View(education);
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
            //Data tidak ditemukan
        }
        else
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}
