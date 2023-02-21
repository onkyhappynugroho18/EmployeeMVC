﻿using EmployeeMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Contexts;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext>context) : base(context)
    {

    }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Profiling> Profilings { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<University> Universities { get; set; }

    //Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //membuat atribut jadi unique
        modelBuilder.Entity<Employee>().HasIndex(e => new
        {
            e.Email,
            e.PhoneNumber
        }).IsUnique();

        // Relasi one Employee ke one Account sekaligus menjadi Primary Key
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Account)
            .WithOne(a => a.Employee)
            .HasForeignKey<Account>(fk => fk.EmployeeNIK);
    }
}
