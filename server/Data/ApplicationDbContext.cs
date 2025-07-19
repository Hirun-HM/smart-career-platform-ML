using Microsoft.EntityFrameworkCore;
using SmartCareerPlatform.Data;
using SmartCareerPlatform.Models;

namespace SmartCareerPlatform
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.Skills)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "UserSkill",
                    j => j.HasOne<Skill>().WithMany().HasForeignKey("SkillId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "SkillId");
                        j.ToTable("UserSkills");
                    });

            // Seed skills
            SkillSeeder.SeedSkills(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
