using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartCareerPlatform.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Skills = table.Column<List<string>>(type: "text[]", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<decimal>(type: "numeric", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    RelatedSkills = table.Column<List<string>>(type: "text[]", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Interests = table.Column<List<string>>(type: "text[]", nullable: false),
                    Experience = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    EnrolledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Progress = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCourse_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourse_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSkills",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SkillId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkills", x => new { x.UserId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_UserSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSkills_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "Name", "RelatedSkills" },
                values: new object[,]
                {
                    { 1, "Programming Languages", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5220), "Dynamic programming language for web development", "JavaScript", new List<string>() },
                    { 2, "Programming Languages", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5220), "General-purpose programming language", "Python", new List<string>() },
                    { 3, "Programming Languages", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5230), "Object-oriented programming language", "Java", new List<string>() },
                    { 4, "Programming Languages", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5230), "Microsoft's object-oriented programming language", "C#", new List<string>() },
                    { 5, "Programming Languages", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5230), "Typed superset of JavaScript", "TypeScript", new List<string>() },
                    { 6, "Programming Languages", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5230), "Google's systems programming language", "Go", new List<string>() },
                    { 7, "Programming Languages", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5230), "Systems programming language focused on safety", "Rust", new List<string>() },
                    { 8, "Programming Languages", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5230), "Extension of C programming language", "C++", new List<string>() },
                    { 9, "Programming Languages", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5230), "Server-side scripting language", "PHP", new List<string>() },
                    { 10, "Programming Languages", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5240), "Dynamic programming language", "Ruby", new List<string>() },
                    { 11, "Programming Languages", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5240), "Apple's programming language for iOS/macOS", "Swift", new List<string>() },
                    { 12, "Programming Languages", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5240), "Modern programming language for Android", "Kotlin", new List<string>() },
                    { 13, "Programming Languages", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5240), "Functional and object-oriented programming language", "Scala", new List<string>() },
                    { 14, "Programming Languages", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5240), "Statistical computing language", "R", new List<string>() },
                    { 15, "Programming Languages", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5240), "Numerical computing language", "MATLAB", new List<string>() },
                    { 16, "Frontend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5240), "JavaScript library for building user interfaces", "React", new List<string>() },
                    { 17, "Frontend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5240), "Progressive JavaScript framework", "Vue.js", new List<string>() },
                    { 18, "Frontend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), "TypeScript-based web application framework", "Angular", new List<string>() },
                    { 19, "Frontend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), "Markup language for web pages", "HTML5", new List<string>() },
                    { 20, "Frontend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), "Stylesheet language for web design", "CSS3", new List<string>() },
                    { 21, "Frontend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), "CSS preprocessor", "SASS/SCSS", new List<string>() },
                    { 22, "Frontend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), "CSS framework for responsive design", "Bootstrap", new List<string>() },
                    { 23, "Frontend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), "Utility-first CSS framework", "Tailwind CSS", new List<string>() },
                    { 24, "Frontend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), "JavaScript library for DOM manipulation", "jQuery", new List<string>() },
                    { 25, "Frontend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), "Module bundler for JavaScript applications", "Webpack", new List<string>() },
                    { 26, "Frontend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), "Fast build tool for modern web projects", "Vite", new List<string>() },
                    { 27, "Frontend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), "React framework for production", "Next.js", new List<string>() },
                    { 28, "Frontend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), "Vue.js framework for universal applications", "Nuxt.js", new List<string>() },
                    { 29, "Frontend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), "Compile-time framework for web apps", "Svelte", new List<string>() },
                    { 30, "Frontend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), "State management library for JavaScript apps", "Redux", new List<string>() },
                    { 31, "Backend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), "JavaScript runtime for server-side development", "Node.js", new List<string>() },
                    { 32, "Backend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), "Web framework for Node.js", "Express.js", new List<string>() },
                    { 33, "Backend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), "Python web framework", "Django", new List<string>() },
                    { 34, "Backend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), "Lightweight Python web framework", "Flask", new List<string>() },
                    { 35, "Backend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), "Java framework for building applications", "Spring Boot", new List<string>() },
                    { 36, "Backend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), "Microsoft's web framework", "ASP.NET Core", new List<string>() },
                    { 37, "Backend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), "Ruby web framework", "Ruby on Rails", new List<string>() },
                    { 38, "Backend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), "PHP web framework", "Laravel", new List<string>() },
                    { 39, "Backend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), "Modern Python web framework", "FastAPI", new List<string>() },
                    { 40, "Backend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), "HTTP web framework for Go", "Gin", new List<string>() },
                    { 41, "Backend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5270), "Express-inspired web framework for Go", "Fiber", new List<string>() },
                    { 42, "Backend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5270), "Actor framework for Rust", "Actix", new List<string>() },
                    { 43, "Backend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5270), "Node.js framework for scalable applications", "Nest.js", new List<string>() },
                    { 44, "Backend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5270), "Next generation web framework for Node.js", "Koa.js", new List<string>() },
                    { 45, "Backend", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5270), "Rich framework for building applications", "Hapi.js", new List<string>() },
                    { 46, "Database", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5270), "Structured Query Language", "SQL", new List<string>() },
                    { 47, "Database", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), "Open source relational database", "PostgreSQL", new List<string>() },
                    { 48, "Database", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), "Popular relational database", "MySQL", new List<string>() },
                    { 49, "Database", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), "NoSQL document database", "MongoDB", new List<string>() },
                    { 50, "Database", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), "In-memory data structure store", "Redis", new List<string>() },
                    { 51, "Database", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), "Lightweight relational database", "SQLite", new List<string>() },
                    { 52, "Database", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), "Enterprise relational database", "Oracle", new List<string>() },
                    { 53, "Database", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), "Distributed NoSQL database", "Cassandra", new List<string>() },
                    { 54, "Database", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), "AWS NoSQL database service", "DynamoDB", new List<string>() },
                    { 55, "Database", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), "Search and analytics engine", "Elasticsearch", new List<string>() },
                    { 56, "Database", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), "Graph database platform", "Neo4j", new List<string>() },
                    { 57, "Database", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), "Time series database", "InfluxDB", new List<string>() },
                    { 58, "Database", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), "Document-oriented NoSQL database", "CouchDB", new List<string>() },
                    { 59, "Database", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), "Microsoft's relational database", "Microsoft SQL Server", new List<string>() },
                    { 60, "Database", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), "Google's mobile and web development platform", "Firebase", new List<string>() },
                    { 61, "Cloud", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), "Amazon Web Services cloud platform", "AWS", new List<string>() },
                    { 62, "Cloud", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), "Microsoft's cloud computing platform", "Azure", new List<string>() },
                    { 63, "Cloud", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), "Google's cloud computing services", "Google Cloud", new List<string>() },
                    { 64, "DevOps", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), "Containerization platform", "Docker", new List<string>() },
                    { 65, "DevOps", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), "Container orchestration platform", "Kubernetes", new List<string>() },
                    { 66, "DevOps", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), "Automation server for CI/CD", "Jenkins", new List<string>() },
                    { 67, "DevOps", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), "GitLab's CI/CD platform", "GitLab CI", new List<string>() },
                    { 68, "DevOps", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), "GitHub's CI/CD platform", "GitHub Actions", new List<string>() },
                    { 69, "DevOps", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), "Infrastructure as Code tool", "Terraform", new List<string>() },
                    { 70, "DevOps", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), "Configuration management tool", "Ansible", new List<string>() },
                    { 71, "DevOps", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), "Configuration management platform", "Chef", new List<string>() },
                    { 72, "DevOps", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5300), "Configuration management tool", "Puppet", new List<string>() },
                    { 73, "DevOps", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5300), "Development environment manager", "Vagrant", new List<string>() },
                    { 74, "DevOps", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5300), "Web server and reverse proxy", "Nginx", new List<string>() },
                    { 75, "DevOps", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5300), "Web server software", "Apache", new List<string>() },
                    { 76, "Mobile", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5300), "Cross-platform mobile development", "React Native", new List<string>() },
                    { 77, "Mobile", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5300), "Google's UI toolkit for mobile apps", "Flutter", new List<string>() },
                    { 78, "Mobile", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5300), "Native iOS app development", "iOS Development", new List<string>() },
                    { 79, "Mobile", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), "Native Android app development", "Android Development", new List<string>() },
                    { 80, "Mobile", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), "Microsoft's cross-platform framework", "Xamarin", new List<string>() },
                    { 81, "Mobile", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), "Hybrid mobile app development", "Cordova", new List<string>() },
                    { 82, "Mobile", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), "Cross-platform mobile framework", "Ionic", new List<string>() },
                    { 83, "Mobile", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), "Game development platform", "Unity", new List<string>() },
                    { 84, "Mobile", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), "Game development engine", "Unreal Engine", new List<string>() },
                    { 85, "Mobile", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), "Apple's augmented reality framework", "ARKit", new List<string>() },
                    { 86, "AI/ML", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), "Algorithms that learn from data", "Machine Learning", new List<string>() },
                    { 87, "AI/ML", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), "Neural networks with multiple layers", "Deep Learning", new List<string>() },
                    { 88, "AI/ML", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), "Open source ML platform", "TensorFlow", new List<string>() },
                    { 89, "AI/ML", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), "Deep learning framework", "PyTorch", new List<string>() },
                    { 90, "AI/ML", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), "Machine learning library for Python", "Scikit-learn", new List<string>() },
                    { 91, "Data Science", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), "Data manipulation library", "Pandas", new List<string>() },
                    { 92, "Data Science", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), "Numerical computing library", "NumPy", new List<string>() },
                    { 93, "Data Science", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), "Data visualization library", "Matplotlib", new List<string>() },
                    { 94, "Data Science", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), "Statistical data visualization", "Seaborn", new List<string>() },
                    { 95, "Data Science", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), "Interactive computing environment", "Jupyter", new List<string>() },
                    { 96, "Big Data", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), "Unified analytics engine", "Apache Spark", new List<string>() },
                    { 97, "Big Data", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), "Distributed storage and processing", "Hadoop", new List<string>() },
                    { 98, "Big Data", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), "Distributed streaming platform", "Kafka", new List<string>() },
                    { 99, "Data Visualization", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), "Data visualization software", "Tableau", new List<string>() },
                    { 100, "Data Visualization", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), "Microsoft's business analytics tool", "Power BI", new List<string>() },
                    { 101, "Testing", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), "JavaScript testing framework", "Jest", new List<string>() },
                    { 102, "Testing", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), "End-to-end testing framework", "Cypress", new List<string>() },
                    { 103, "Testing", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), "Web browser automation", "Selenium", new List<string>() },
                    { 104, "Testing", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), "Unit testing framework for Java", "JUnit", new List<string>() },
                    { 105, "Testing", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), "Testing framework for Python", "PyTest", new List<string>() },
                    { 106, "Testing", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), "JavaScript test framework", "Mocha", new List<string>() },
                    { 107, "Testing", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), "Behavior-driven testing framework", "Jasmine", new List<string>() },
                    { 108, "Testing", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), "Testing framework for Java", "TestNG", new List<string>() },
                    { 109, "Testing", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), "API testing platform", "Postman", new List<string>() },
                    { 110, "Testing", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), "API testing tool", "Insomnia", new List<string>() },
                    { 111, "Version Control", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), "Distributed version control system", "Git", new List<string>() },
                    { 112, "Version Control", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), "Git repository hosting service", "GitHub", new List<string>() },
                    { 113, "Version Control", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), "Git repository management platform", "GitLab", new List<string>() },
                    { 114, "Version Control", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), "Git repository hosting service", "Bitbucket", new List<string>() },
                    { 115, "Version Control", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), "Centralized version control system", "SVN", new List<string>() },
                    { 116, "Tools", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), "Code editor by Microsoft", "VS Code", new List<string>() },
                    { 117, "Tools", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), "Java IDE by JetBrains", "IntelliJ IDEA", new List<string>() },
                    { 118, "Tools", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), "Microsoft's IDE", "Visual Studio", new List<string>() },
                    { 119, "Tools", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), "Text editor for code", "Sublime Text", new List<string>() },
                    { 120, "Tools", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), "Text editor", "Vim", new List<string>() },
                    { 121, "Soft Skills", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), "Planning and executing projects", "Project Management", new List<string>() },
                    { 122, "Methodology", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), "Iterative development methodology", "Agile", new List<string>() },
                    { 123, "Methodology", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), "Agile framework for project management", "Scrum", new List<string>() },
                    { 124, "Methodology", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), "Visual workflow management", "Kanban", new List<string>() },
                    { 125, "Soft Skills", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), "Guiding and inspiring teams", "Leadership", new List<string>() },
                    { 126, "Soft Skills", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), "Effective information exchange", "Communication", new List<string>() },
                    { 127, "Soft Skills", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), "Analytical thinking and solutions", "Problem Solving", new List<string>() },
                    { 128, "Soft Skills", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), "Working effectively with others", "Team Collaboration", new List<string>() },
                    { 129, "Soft Skills", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), "Efficiently managing time and tasks", "Time Management", new List<string>() },
                    { 130, "Soft Skills", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), "Objective analysis and evaluation", "Critical Thinking", new List<string>() },
                    { 131, "Security", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), "Protecting digital systems", "Cybersecurity", new List<string>() },
                    { 132, "Security", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), "Simulated cyber attacks", "Penetration Testing", new List<string>() },
                    { 133, "Security", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), "Secure communication techniques", "Cryptography", new List<string>() },
                    { 134, "Security", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), "Protecting network infrastructure", "Network Security", new List<string>() },
                    { 135, "Security", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), "Web application security", "OWASP", new List<string>() },
                    { 136, "Security", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), "Authorization framework", "OAuth", new List<string>() },
                    { 137, "Security", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), "JSON Web Tokens", "JWT", new List<string>() },
                    { 138, "Security", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), "Secure communication protocols", "SSL/TLS", new List<string>() },
                    { 139, "Security", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), "Network security system", "Firewall", new List<string>() },
                    { 140, "Security", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), "Identifying security weaknesses", "Vulnerability Assessment", new List<string>() },
                    { 141, "Emerging Tech", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), "Distributed ledger technology", "Blockchain", new List<string>() },
                    { 142, "Emerging Tech", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), "Smart contract programming language", "Solidity", new List<string>() },
                    { 143, "Emerging Tech", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), "Internet of Things development", "IoT", new List<string>() },
                    { 144, "Emerging Tech", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), "Augmented and Virtual Reality", "AR/VR", new List<string>() },
                    { 145, "Emerging Tech", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), "Quantum-mechanical computing", "Quantum Computing", new List<string>() },
                    { 146, "Emerging Tech", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), "Distributed computing paradigm", "Edge Computing", new List<string>() },
                    { 147, "Architecture", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), "Distributed system architecture", "Microservices", new List<string>() },
                    { 148, "API", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5360), "Query language for APIs", "GraphQL", new List<string>() },
                    { 149, "API", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5360), "Representational State Transfer", "REST API", new List<string>() },
                    { 150, "API", new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5360), "Real-time communication protocol", "WebSockets", new List<string>() }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCourse_CourseId",
                table: "UserCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourse_UserId",
                table: "UserCourse",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_SkillId",
                table: "UserSkills",
                column: "SkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCourse");

            migrationBuilder.DropTable(
                name: "UserSkills");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
