using EmployeeMVC.Contexts;
using EmployeeMVC.Models;
using EmployeeMVC.Repositories.Interface;
using EmployeeMVC.ViewModels;

namespace EmployeeMVC.Repositories;

public class EmployeeRepository : IRepository<int, Employee>
{
    private MyContext context;
    public EmployeeRepository(MyContext context)
    {
        this.context = context;
    }
    public int Delete(int key)
    {
        int result = 0;
        var employee = GetById(key);
        if (employee == null)
        {
            return result;
        }
        context.Remove(employee);
        result = context.SaveChanges();
        return result;
    }

    public List<Employee> GetAll()
    {
        return context.Employees.ToList() ?? null;
    }

    public Employee GetById(int key)
    {
        return context.Employees.Find(key) ?? null;;
    }

    public int Insert(Employee entity)
    {
        throw new NotImplementedException();
    }

    public int Update(Employee entity)
    {
        throw new NotImplementedException();
    }

    public List<EmployeeVM> GetEmployee()
    {

        var results = context.Employees.Select(e => new EmployeeVM
        {
            NIK = e.NIK,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Birthdate = e.Birthdate,
            Gender = e.Gender,
            HiringDate = e.HiringDate,
            Email = e.Email,
            PhoneNumber = e.PhoneNumber
        }).ToList();
        return results;
    }
    public EmployeeVM GetEmployeeById(int key)
    {
        var e = GetById(key);
        var results = new EmployeeVM
        {
            NIK = e.NIK,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Birthdate = e.Birthdate,
            Gender = e.Gender,
            HiringDate = e.HiringDate,
            Email = e.Email,
            PhoneNumber = e.PhoneNumber
        };
        return results;
    }

}
