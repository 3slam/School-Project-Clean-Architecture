﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using School.Data.Entities;

namespace School.Infrastructure;

public partial class ApplicationDatabaseContext : IdentityDbContext
{
    public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options)
        : base(options)
    {
    }

   
    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Department>().HasData(
           new Department { DepartmentId = 1, DeptName = "Computer Science", DeptDesc = "Department of Computer Science", DeptLocation = "Building A", DeptManager = 1, ManagerHiredate = new DateOnly(2020, 1, 15) },
           new Department { DepartmentId = 2, DeptName = "Mathematics", DeptDesc = "Department of Mathematics", DeptLocation = "Building B", DeptManager = 2, ManagerHiredate = new DateOnly(2020, 1, 15) }
       );
        base.OnModelCreating(modelBuilder);
    }

}