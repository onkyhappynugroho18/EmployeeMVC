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
    private readonly EducationRepository repository;
    private readonly UniversityRepository universityRepository;

    public EducationController(MyContext context, EducationRepository repository, UniversityRepository universityRepository)
    {
        this.context = context;
        this.repository = repository;
        this.universityRepository = universityRepository;
    }
    public IActionResult Index()
    {
        var results = repository.GetEducationUniversities();
        return View(results);
    }
    public IActionResult Details(int id)
    {
        var educations = repository.GetEduUnivById(id);
        return View(educations);
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EducationUniversityVM education)
    {
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
    public IActionResult Delete(int id)
    {
        var education = repository.GetById(id);
        return View(education);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
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
