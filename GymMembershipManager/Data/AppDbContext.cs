using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GymMembershipManager.Models;

namespace GymMembershipManager.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<GymEquipment> GymEquipments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gym.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 100,
                Username = "admin",
                PasswordHash = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                Role = "Admin"
            });
        }
    }
}
