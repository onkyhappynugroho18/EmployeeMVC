using EmployeeMVC.Contexts;
using EmployeeMVC.Models;
using EmployeeMVC.Repositories.Interface;
using EmployeeMVC.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Repositories;

public class EducationRepository : IRepository<int, Education>
{
    private readonly MyContext context;
    private readonly UniversityRepository universityRepository;


    public EducationRepository(MyContext context, UniversityRepository universityRepository)
    {
        this.context = context;
        this.universityRepository = universityRepository;
    }

    public int Delete(int key)
    {
        int result = 0;
        var education = GetById(key);
        if (education == null)
        {
            return result;
        }
        context.Remove(education);
        result = context.SaveChanges();
        return result;
    }

    public List<Education> GetAll()
    {
        return context.Educations.ToList();
    }

    public Education GetById(int key)
    {
        return context.Educations.Find(key) ?? null;
    }

    public int Insert(Education entity)
    {
        int result = 0;
        context.Add(entity);
        result = context.SaveChanges();
        return result;
    }

    public int Update(Education entity)
    {
        int result = 0;
        context.Entry(entity).State = EntityState.Modified;
        result = context.SaveChanges();
        return result;
    }
    public List<EducationUniversityVM> GetEducationUniversities()
    {
        var results = (from e in GetAll()
                       join u in universityRepository.GetAll()
                       on e.UniversityId equals u.Id
                       select new EducationUniversityVM
                       {
                           Id = e.Id,
                           Degree = e.Degree,
                           Gpa = e.Gpa,
                           Major = e.Major,
                           UniversityName = u.Name
                       }).ToList();
        return results;
    }

    public EducationUniversityVM GetEduUnivById(int key)
    {
        var education = GetById(key);
        var results = new EducationUniversityVM
        {
            Id = education.Id,
            Degree = education.Degree,
            Gpa = education.Gpa,
            Major = education.Major,
            UniversityName = universityRepository.GetById(education.UniversityId).Name
        };
        return results;
    }
}
