using EmployeeMVC.Contexts;
using EmployeeMVC.Models;
using EmployeeMVC.Repositories.Interface;
using EmployeeMVC.ViewModels;

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
        throw new NotImplementedException();
    }

    public List<Education> GetAll()
    {
        return context.Educations.ToList();
    }

    public Education GetById(int key)
    {
        throw new NotImplementedException();
    }

    public int Insert(Education entity)
    {
        throw new NotImplementedException();
    }

    public int Update(Education entity)
    {
        throw new NotImplementedException();
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

}
