using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EnterpriseAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EnterpriseAPI.Context
{
    public class TakeCareDBContext : IdentityDbContext<IdentityUser>
    {
        public TakeCareDBContext(DbContextOptions<TakeCareDBContext> options) : base(options)
        {

        }
        
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
