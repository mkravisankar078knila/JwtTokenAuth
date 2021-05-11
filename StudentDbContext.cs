using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JwtToken.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace JwtToken.DAL
{
    public class StudentDbContext : IdentityDbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }
        public DbSet<StudentList> StudentLists { get; set; }
        public DbSet<LoginModel> LoginModels { get; set; }
        public DbSet<RegisterMas> registerMas { get; set; }
    }
}