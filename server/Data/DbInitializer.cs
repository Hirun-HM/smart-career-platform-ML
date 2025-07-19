using SmartCareerPlatform;
using SmartCareerPlatform.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using SmartCareerPlatform.Server.Data; // Add this if ApplicationDbContext is in this namespace

namespace SmartCareerPlatform.Server.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
         
            context.Database.EnsureCreated();

           
            if (!context.Users.Any())
            {
                var hashedPassword = HashPassword("password");
                context.Users.Add(new User { Username = "test", PasswordHash = hashedPassword, Email = "test@example.com", Experience = 2 });
                context.SaveChanges();
            }

            
            if (!context.Courses.Any())
            {
                context.Courses.Add(new Course { Title = "Introduction to AI", Description = "Learn the basics of Artificial Intelligence.", Duration = 10 });
                context.Courses.Add(new Course { Title = "Web Development Bootcamp", Description = "Become a full-stack web developer.", Duration = 20 });
                context.SaveChanges();
            }

           
            if (!context.Skills.Any())
            {
                context.Skills.Add(new Skill { Name = "Machine Learning" });
                context.Skills.Add(new Skill { Name = "AI" });
                context.Skills.Add(new Skill { Name = "HTML" });
                context.Skills.Add(new Skill { Name = "CSS" });
                context.Skills.Add(new Skill { Name = "JavaScript" });
                context.SaveChanges();
            }
        }

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
