using EmployeeMVC.Contexts;
using EmployeeMVC.Models;
using EmployeeMVC.Repositories.Interface;
using EmployeeMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Repositories;

public class EmployeeRepository : IRepository<string, Employee>
{
    private MyContext context;
    public EmployeeRepository(MyContext context)
    {
        this.context = context;
    }
    public int Delete(string key)
    {
        var entity = GetById(key);
        context.Remove(entity);
        return context.SaveChanges();
    }

    public List<Employee> GetAll()
    {
        return context.Employees.ToList() ?? null;
    }

    public Employee GetById(string key)
    {
        return context.Employees.Find(key) ?? null;
    }

    public int Insert(Employee entity)
    {
        int result = 0;
        context.Add(entity);
        result = context.SaveChanges();
        return result;
    }

    public int Update(Employee entity)
    {
        var result = context.Entry(entity).State = EntityState.Modified;
        return context.SaveChanges();
    }
}
