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

            modelBuilder.Entity<User>().HasData(
     new User
     {
         Id = 999,
         Username = "manager",
         PasswordHash = "866485796cfa8d7c0cf7111640205b83076433547577511d81f8030ae99ecea5",
         Role = "Manager"
     },
     new User
     {
         Id = 1000,
         Username = "radnik",
         PasswordHash = "73203dfc63612de279e2757774b5616706040fddc6c098b1e0b4561c2b9ab0ba",
         Role = "Radnik"
     }
 );
        }
    }
}
