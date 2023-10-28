using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Infrastructure.DBContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<HackHeroes.Domain.Entities.Class> Classes { get; set; }
        public DbSet<HackHeroes.Domain.Entities.Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HackHeroes.Domain.Entities.Class>()
                .HasMany(c => c.Students)
                .WithOne(c => c.Class)
                .HasForeignKey(s => s.ClassId);
        }
    }
}
