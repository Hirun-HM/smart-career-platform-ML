using Microsoft.EntityFrameworkCore;
using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Data
{
    public static class CourseSeeder
    {
        public static void SeedCourses(ApplicationDbContext context)
        {
            if (context.Courses.Any())
                return; // Database has been seeded

            var courses = new List<Course>
            {
                new Course
                {
                    Title = "Complete JavaScript Course 2024: From Zero to Expert!",
                    Description = "The modern JavaScript course for everyone! Master JavaScript with projects, challenges and theory. Many courses in one!",
                    Provider = "Udemy",
                    Instructor = "Jonas Schmedtmann",
                    Price = 89.99m,
                    IsActive = true,
                    Duration = 69,
                    Level = "Beginner to Advanced",
                    Rating = 4.7m,
                    Category = "Programming",
                    Skills = new List<string> { "JavaScript", "Web Development", "ES6", "DOM Manipulation", "Async Programming" },
                    Url = "https://www.udemy.com/course/the-complete-javascript-course/",
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Title = "React - The Complete Guide 2024 (incl. React Router & Redux)",
                    Description = "Dive in and learn React.js from scratch! Learn Reactjs, Hooks, Redux, React Routing, Animations, Next.js and way more!",
                    Provider = "Udemy",
                    Instructor = "Maximilian Schwarzmüller",
                    Price = 94.99m,
                    IsActive = true,
                    Duration = 48,
                    Level = "Intermediate",
                    Rating = 4.6m,
                    Category = "Frontend Development",
                    Skills = new List<string> { "React", "Redux", "React Router", "JSX", "Hooks", "JavaScript" },
                    Url = "https://www.udemy.com/course/react-the-complete-guide-incl-redux/",
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Title = "Python for Everybody Specialization",
                    Description = "Learn to Program and Analyze Data with Python. Develop programs to gather, clean, analyze, and visualize data.",
                    Provider = "Coursera",
                    Instructor = "Charles Russell Severance",
                    Price = 49.00m,
                    IsActive = true,
                    Duration = 32,
                    Level = "Beginner",
                    Rating = 4.8m,
                    Category = "Programming",
                    Skills = new List<string> { "Python", "Data Analysis", "Web Scraping", "Database Programming", "Data Visualization" },
                    Url = "https://www.coursera.org/specializations/python",
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Title = "Machine Learning Specialization",
                    Description = "Build ML models with NumPy & scikit-learn, build & train supervised models for prediction & binary classification tasks.",
                    Provider = "Coursera",
                    Instructor = "Andrew Ng",
                    Price = 79.00m,
                    IsActive = true,
                    Duration = 42,
                    Level = "Intermediate",
                    Rating = 4.9m,
                    Category = "Machine Learning",
                    Skills = new List<string> { "Machine Learning", "Python", "Scikit-learn", "TensorFlow", "Neural Networks", "Deep Learning" },
                    Url = "https://www.coursera.org/specializations/machine-learning-introduction",
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Title = "AWS Certified Solutions Architect - Associate 2024",
                    Description = "Pass the AWS Certified Solutions Architect Associate Certification SAA-C03. Complete Amazon Web Services Cloud Training Course.",
                    Provider = "Udemy",
                    Instructor = "Stephane Maarek",
                    Price = 84.99m,
                    IsActive = true,
                    Duration = 27,
                    Level = "Intermediate",
                    Rating = 4.7m,
                    Category = "Cloud Computing",
                    Skills = new List<string> { "AWS", "Cloud Architecture", "EC2", "S3", "VPC", "Lambda", "RDS", "DynamoDB" },
                    Url = "https://www.udemy.com/course/aws-certified-solutions-architect-associate/",
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Title = "The Complete Node.js Developer Course (3rd Edition)",
                    Description = "Learn Node.js by building real-world applications with Node JS, Express, MongoDB, Jest, and more!",
                    Provider = "Udemy",
                    Instructor = "Andrew Mead",
                    Price = 89.99m,
                    IsActive = true,
                    Duration = 35,
                    Level = "Intermediate",
                    Rating = 4.7m,
                    Category = "Backend Development",
                    Skills = new List<string> { "Node.js", "Express.js", "MongoDB", "REST API", "GraphQL", "JWT", "Socket.io" },
                    Url = "https://www.udemy.com/course/the-complete-nodejs-developer-course-2/",
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Title = "Complete SQL and Databases Bootcamp: Zero to Mastery",
                    Description = "Master SQL, Database Design, PostgreSQL, MySQL and learn to build applications using Python + SQL!",
                    Provider = "Udemy",
                    Instructor = "Andrei Neagoie",
                    Price = 74.99m,
                    IsActive = true,
                    Duration = 22,
                    Level = "Beginner to Advanced",
                    Rating = 4.6m,
                    Category = "Database",
                    Skills = new List<string> { "SQL", "PostgreSQL", "MySQL", "Database Design", "Query Optimization", "Python" },
                    Url = "https://www.udemy.com/course/complete-sql-databases-bootcamp-zero-to-mastery/",
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Title = "Cybersecurity for Beginners",
                    Description = "Learn cybersecurity fundamentals, network security, ethical hacking, and how to protect systems from cyber threats.",
                    Provider = "edX",
                    Instructor = "New York University",
                    Price = 0.00m,
                    IsActive = true,
                    Duration = 18,
                    Level = "Beginner",
                    Rating = 4.5m,
                    Category = "Cybersecurity",
                    Skills = new List<string> { "Cybersecurity", "Network Security", "Ethical Hacking", "Risk Assessment", "Incident Response" },
                    Url = "https://www.edx.org/course/cybersecurity-fundamentals",
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Title = "Data Science Specialization",
                    Description = "Launch your career in data science. Learn to clean, analyze, and visualize data using R programming language.",
                    Provider = "Coursera",
                    Instructor = "Johns Hopkins University",
                    Price = 59.00m,
                    IsActive = true,
                    Duration = 55,
                    Level = "Intermediate",
                    Rating = 4.5m,
                    Category = "Data Science",
                    Skills = new List<string> { "Data Science", "R Programming", "Data Visualization", "Statistical Analysis", "Machine Learning" },
                    Url = "https://www.coursera.org/specializations/jhu-data-science",
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Title = "Flutter & Dart - The Complete Guide [2024 Edition]",
                    Description = "A Complete Guide to the Flutter SDK & Flutter Framework for building native iOS and Android apps",
                    Provider = "Udemy",
                    Instructor = "Maximilian Schwarzmüller",
                    Price = 99.99m,
                    IsActive = true,
                    Duration = 42,
                    Level = "Beginner to Advanced",
                    Rating = 4.6m,
                    Category = "Mobile Development",
                    Skills = new List<string> { "Flutter", "Dart", "Mobile Development", "iOS Development", "Android Development", "Firebase" },
                    Url = "https://www.udemy.com/course/learn-flutter-dart-to-build-ios-android-apps/",
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();
        }
    }
}
