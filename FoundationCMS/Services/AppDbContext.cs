using FoundationCMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoundationCMS.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<EventDonor> EventDonors { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Contribution> Contributions { get; set; }

    }
}
