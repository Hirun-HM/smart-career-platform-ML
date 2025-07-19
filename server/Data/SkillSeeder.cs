using Microsoft.EntityFrameworkCore;
using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Data
{
    public static class SkillSeeder
    {
        public static void SeedSkills(ModelBuilder modelBuilder)
        {
            var skills = new List<Skill>
            {
                // Programming Languages
                new Skill { Id = 1, Name = "JavaScript", Category = "Programming Languages", Description = "Dynamic programming language for web development" },
                new Skill { Id = 2, Name = "Python", Category = "Programming Languages", Description = "General-purpose programming language" },
                new Skill { Id = 3, Name = "Java", Category = "Programming Languages", Description = "Object-oriented programming language" },
                new Skill { Id = 4, Name = "C#", Category = "Programming Languages", Description = "Microsoft's object-oriented programming language" },
                new Skill { Id = 5, Name = "TypeScript", Category = "Programming Languages", Description = "Typed superset of JavaScript" },
                new Skill { Id = 6, Name = "Go", Category = "Programming Languages", Description = "Google's systems programming language" },
                new Skill { Id = 7, Name = "Rust", Category = "Programming Languages", Description = "Systems programming language focused on safety" },
                new Skill { Id = 8, Name = "C++", Category = "Programming Languages", Description = "Extension of C programming language" },
                new Skill { Id = 9, Name = "PHP", Category = "Programming Languages", Description = "Server-side scripting language" },
                new Skill { Id = 10, Name = "Ruby", Category = "Programming Languages", Description = "Dynamic programming language" },
                new Skill { Id = 11, Name = "Swift", Category = "Programming Languages", Description = "Apple's programming language for iOS/macOS" },
                new Skill { Id = 12, Name = "Kotlin", Category = "Programming Languages", Description = "Modern programming language for Android" },
                new Skill { Id = 13, Name = "Scala", Category = "Programming Languages", Description = "Functional and object-oriented programming language" },
                new Skill { Id = 14, Name = "R", Category = "Programming Languages", Description = "Statistical computing language" },
                new Skill { Id = 15, Name = "MATLAB", Category = "Programming Languages", Description = "Numerical computing language" },

                // Frontend Technologies
                new Skill { Id = 16, Name = "React", Category = "Frontend", Description = "JavaScript library for building user interfaces" },
                new Skill { Id = 17, Name = "Vue.js", Category = "Frontend", Description = "Progressive JavaScript framework" },
                new Skill { Id = 18, Name = "Angular", Category = "Frontend", Description = "TypeScript-based web application framework" },
                new Skill { Id = 19, Name = "HTML5", Category = "Frontend", Description = "Markup language for web pages" },
                new Skill { Id = 20, Name = "CSS3", Category = "Frontend", Description = "Stylesheet language for web design" },
                new Skill { Id = 21, Name = "SASS/SCSS", Category = "Frontend", Description = "CSS preprocessor" },
                new Skill { Id = 22, Name = "Bootstrap", Category = "Frontend", Description = "CSS framework for responsive design" },
                new Skill { Id = 23, Name = "Tailwind CSS", Category = "Frontend", Description = "Utility-first CSS framework" },
                new Skill { Id = 24, Name = "jQuery", Category = "Frontend", Description = "JavaScript library for DOM manipulation" },
                new Skill { Id = 25, Name = "Webpack", Category = "Frontend", Description = "Module bundler for JavaScript applications" },
                new Skill { Id = 26, Name = "Vite", Category = "Frontend", Description = "Fast build tool for modern web projects" },
                new Skill { Id = 27, Name = "Next.js", Category = "Frontend", Description = "React framework for production" },
                new Skill { Id = 28, Name = "Nuxt.js", Category = "Frontend", Description = "Vue.js framework for universal applications" },
                new Skill { Id = 29, Name = "Svelte", Category = "Frontend", Description = "Compile-time framework for web apps" },
                new Skill { Id = 30, Name = "Redux", Category = "Frontend", Description = "State management library for JavaScript apps" },

                // Backend Technologies
                new Skill { Id = 31, Name = "Node.js", Category = "Backend", Description = "JavaScript runtime for server-side development" },
                new Skill { Id = 32, Name = "Express.js", Category = "Backend", Description = "Web framework for Node.js" },
                new Skill { Id = 33, Name = "Django", Category = "Backend", Description = "Python web framework" },
                new Skill { Id = 34, Name = "Flask", Category = "Backend", Description = "Lightweight Python web framework" },
                new Skill { Id = 35, Name = "Spring Boot", Category = "Backend", Description = "Java framework for building applications" },
                new Skill { Id = 36, Name = "ASP.NET Core", Category = "Backend", Description = "Microsoft's web framework" },
                new Skill { Id = 37, Name = "Ruby on Rails", Category = "Backend", Description = "Ruby web framework" },
                new Skill { Id = 38, Name = "Laravel", Category = "Backend", Description = "PHP web framework" },
                new Skill { Id = 39, Name = "FastAPI", Category = "Backend", Description = "Modern Python web framework" },
                new Skill { Id = 40, Name = "Gin", Category = "Backend", Description = "HTTP web framework for Go" },
                new Skill { Id = 41, Name = "Fiber", Category = "Backend", Description = "Express-inspired web framework for Go" },
                new Skill { Id = 42, Name = "Actix", Category = "Backend", Description = "Actor framework for Rust" },
                new Skill { Id = 43, Name = "Nest.js", Category = "Backend", Description = "Node.js framework for scalable applications" },
                new Skill { Id = 44, Name = "Koa.js", Category = "Backend", Description = "Next generation web framework for Node.js" },
                new Skill { Id = 45, Name = "Hapi.js", Category = "Backend", Description = "Rich framework for building applications" },

                // Databases
                new Skill { Id = 46, Name = "SQL", Category = "Database", Description = "Structured Query Language" },
                new Skill { Id = 47, Name = "PostgreSQL", Category = "Database", Description = "Open source relational database" },
                new Skill { Id = 48, Name = "MySQL", Category = "Database", Description = "Popular relational database" },
                new Skill { Id = 49, Name = "MongoDB", Category = "Database", Description = "NoSQL document database" },
                new Skill { Id = 50, Name = "Redis", Category = "Database", Description = "In-memory data structure store" },
                new Skill { Id = 51, Name = "SQLite", Category = "Database", Description = "Lightweight relational database" },
                new Skill { Id = 52, Name = "Oracle", Category = "Database", Description = "Enterprise relational database" },
                new Skill { Id = 53, Name = "Cassandra", Category = "Database", Description = "Distributed NoSQL database" },
                new Skill { Id = 54, Name = "DynamoDB", Category = "Database", Description = "AWS NoSQL database service" },
                new Skill { Id = 55, Name = "Elasticsearch", Category = "Database", Description = "Search and analytics engine" },
                new Skill { Id = 56, Name = "Neo4j", Category = "Database", Description = "Graph database platform" },
                new Skill { Id = 57, Name = "InfluxDB", Category = "Database", Description = "Time series database" },
                new Skill { Id = 58, Name = "CouchDB", Category = "Database", Description = "Document-oriented NoSQL database" },
                new Skill { Id = 59, Name = "Microsoft SQL Server", Category = "Database", Description = "Microsoft's relational database" },
                new Skill { Id = 60, Name = "Firebase", Category = "Database", Description = "Google's mobile and web development platform" },

                // Cloud & DevOps
                new Skill { Id = 61, Name = "AWS", Category = "Cloud", Description = "Amazon Web Services cloud platform" },
                new Skill { Id = 62, Name = "Azure", Category = "Cloud", Description = "Microsoft's cloud computing platform" },
                new Skill { Id = 63, Name = "Google Cloud", Category = "Cloud", Description = "Google's cloud computing services" },
                new Skill { Id = 64, Name = "Docker", Category = "DevOps", Description = "Containerization platform" },
                new Skill { Id = 65, Name = "Kubernetes", Category = "DevOps", Description = "Container orchestration platform" },
                new Skill { Id = 66, Name = "Jenkins", Category = "DevOps", Description = "Automation server for CI/CD" },
                new Skill { Id = 67, Name = "GitLab CI", Category = "DevOps", Description = "GitLab's CI/CD platform" },
                new Skill { Id = 68, Name = "GitHub Actions", Category = "DevOps", Description = "GitHub's CI/CD platform" },
                new Skill { Id = 69, Name = "Terraform", Category = "DevOps", Description = "Infrastructure as Code tool" },
                new Skill { Id = 70, Name = "Ansible", Category = "DevOps", Description = "Configuration management tool" },
                new Skill { Id = 71, Name = "Chef", Category = "DevOps", Description = "Configuration management platform" },
                new Skill { Id = 72, Name = "Puppet", Category = "DevOps", Description = "Configuration management tool" },
                new Skill { Id = 73, Name = "Vagrant", Category = "DevOps", Description = "Development environment manager" },
                new Skill { Id = 74, Name = "Nginx", Category = "DevOps", Description = "Web server and reverse proxy" },
                new Skill { Id = 75, Name = "Apache", Category = "DevOps", Description = "Web server software" },

                // Mobile Development
                new Skill { Id = 76, Name = "React Native", Category = "Mobile", Description = "Cross-platform mobile development" },
                new Skill { Id = 77, Name = "Flutter", Category = "Mobile", Description = "Google's UI toolkit for mobile apps" },
                new Skill { Id = 78, Name = "iOS Development", Category = "Mobile", Description = "Native iOS app development" },
                new Skill { Id = 79, Name = "Android Development", Category = "Mobile", Description = "Native Android app development" },
                new Skill { Id = 80, Name = "Xamarin", Category = "Mobile", Description = "Microsoft's cross-platform framework" },
                new Skill { Id = 81, Name = "Cordova", Category = "Mobile", Description = "Hybrid mobile app development" },
                new Skill { Id = 82, Name = "Ionic", Category = "Mobile", Description = "Cross-platform mobile framework" },
                new Skill { Id = 83, Name = "Unity", Category = "Mobile", Description = "Game development platform" },
                new Skill { Id = 84, Name = "Unreal Engine", Category = "Mobile", Description = "Game development engine" },
                new Skill { Id = 85, Name = "ARKit", Category = "Mobile", Description = "Apple's augmented reality framework" },

                // Data Science & AI
                new Skill { Id = 86, Name = "Machine Learning", Category = "AI/ML", Description = "Algorithms that learn from data" },
                new Skill { Id = 87, Name = "Deep Learning", Category = "AI/ML", Description = "Neural networks with multiple layers" },
                new Skill { Id = 88, Name = "TensorFlow", Category = "AI/ML", Description = "Open source ML platform" },
                new Skill { Id = 89, Name = "PyTorch", Category = "AI/ML", Description = "Deep learning framework" },
                new Skill { Id = 90, Name = "Scikit-learn", Category = "AI/ML", Description = "Machine learning library for Python" },
                new Skill { Id = 91, Name = "Pandas", Category = "Data Science", Description = "Data manipulation library" },
                new Skill { Id = 92, Name = "NumPy", Category = "Data Science", Description = "Numerical computing library" },
                new Skill { Id = 93, Name = "Matplotlib", Category = "Data Science", Description = "Data visualization library" },
                new Skill { Id = 94, Name = "Seaborn", Category = "Data Science", Description = "Statistical data visualization" },
                new Skill { Id = 95, Name = "Jupyter", Category = "Data Science", Description = "Interactive computing environment" },
                new Skill { Id = 96, Name = "Apache Spark", Category = "Big Data", Description = "Unified analytics engine" },
                new Skill { Id = 97, Name = "Hadoop", Category = "Big Data", Description = "Distributed storage and processing" },
                new Skill { Id = 98, Name = "Kafka", Category = "Big Data", Description = "Distributed streaming platform" },
                new Skill { Id = 99, Name = "Tableau", Category = "Data Visualization", Description = "Data visualization software" },
                new Skill { Id = 100, Name = "Power BI", Category = "Data Visualization", Description = "Microsoft's business analytics tool" },

                // Testing
                new Skill { Id = 101, Name = "Jest", Category = "Testing", Description = "JavaScript testing framework" },
                new Skill { Id = 102, Name = "Cypress", Category = "Testing", Description = "End-to-end testing framework" },
                new Skill { Id = 103, Name = "Selenium", Category = "Testing", Description = "Web browser automation" },
                new Skill { Id = 104, Name = "JUnit", Category = "Testing", Description = "Unit testing framework for Java" },
                new Skill { Id = 105, Name = "PyTest", Category = "Testing", Description = "Testing framework for Python" },
                new Skill { Id = 106, Name = "Mocha", Category = "Testing", Description = "JavaScript test framework" },
                new Skill { Id = 107, Name = "Jasmine", Category = "Testing", Description = "Behavior-driven testing framework" },
                new Skill { Id = 108, Name = "TestNG", Category = "Testing", Description = "Testing framework for Java" },
                new Skill { Id = 109, Name = "Postman", Category = "Testing", Description = "API testing platform" },
                new Skill { Id = 110, Name = "Insomnia", Category = "Testing", Description = "API testing tool" },

                // Version Control & Tools
                new Skill { Id = 111, Name = "Git", Category = "Version Control", Description = "Distributed version control system" },
                new Skill { Id = 112, Name = "GitHub", Category = "Version Control", Description = "Git repository hosting service" },
                new Skill { Id = 113, Name = "GitLab", Category = "Version Control", Description = "Git repository management platform" },
                new Skill { Id = 114, Name = "Bitbucket", Category = "Version Control", Description = "Git repository hosting service" },
                new Skill { Id = 115, Name = "SVN", Category = "Version Control", Description = "Centralized version control system" },
                new Skill { Id = 116, Name = "VS Code", Category = "Tools", Description = "Code editor by Microsoft" },
                new Skill { Id = 117, Name = "IntelliJ IDEA", Category = "Tools", Description = "Java IDE by JetBrains" },
                new Skill { Id = 118, Name = "Visual Studio", Category = "Tools", Description = "Microsoft's IDE" },
                new Skill { Id = 119, Name = "Sublime Text", Category = "Tools", Description = "Text editor for code" },
                new Skill { Id = 120, Name = "Vim", Category = "Tools", Description = "Text editor" },

                // Soft Skills
                new Skill { Id = 121, Name = "Project Management", Category = "Soft Skills", Description = "Planning and executing projects" },
                new Skill { Id = 122, Name = "Agile", Category = "Methodology", Description = "Iterative development methodology" },
                new Skill { Id = 123, Name = "Scrum", Category = "Methodology", Description = "Agile framework for project management" },
                new Skill { Id = 124, Name = "Kanban", Category = "Methodology", Description = "Visual workflow management" },
                new Skill { Id = 125, Name = "Leadership", Category = "Soft Skills", Description = "Guiding and inspiring teams" },
                new Skill { Id = 126, Name = "Communication", Category = "Soft Skills", Description = "Effective information exchange" },
                new Skill { Id = 127, Name = "Problem Solving", Category = "Soft Skills", Description = "Analytical thinking and solutions" },
                new Skill { Id = 128, Name = "Team Collaboration", Category = "Soft Skills", Description = "Working effectively with others" },
                new Skill { Id = 129, Name = "Time Management", Category = "Soft Skills", Description = "Efficiently managing time and tasks" },
                new Skill { Id = 130, Name = "Critical Thinking", Category = "Soft Skills", Description = "Objective analysis and evaluation" },

                // Security
                new Skill { Id = 131, Name = "Cybersecurity", Category = "Security", Description = "Protecting digital systems" },
                new Skill { Id = 132, Name = "Penetration Testing", Category = "Security", Description = "Simulated cyber attacks" },
                new Skill { Id = 133, Name = "Cryptography", Category = "Security", Description = "Secure communication techniques" },
                new Skill { Id = 134, Name = "Network Security", Category = "Security", Description = "Protecting network infrastructure" },
                new Skill { Id = 135, Name = "OWASP", Category = "Security", Description = "Web application security" },
                new Skill { Id = 136, Name = "OAuth", Category = "Security", Description = "Authorization framework" },
                new Skill { Id = 137, Name = "JWT", Category = "Security", Description = "JSON Web Tokens" },
                new Skill { Id = 138, Name = "SSL/TLS", Category = "Security", Description = "Secure communication protocols" },
                new Skill { Id = 139, Name = "Firewall", Category = "Security", Description = "Network security system" },
                new Skill { Id = 140, Name = "Vulnerability Assessment", Category = "Security", Description = "Identifying security weaknesses" },

                // Specialized Skills
                new Skill { Id = 141, Name = "Blockchain", Category = "Emerging Tech", Description = "Distributed ledger technology" },
                new Skill { Id = 142, Name = "Solidity", Category = "Emerging Tech", Description = "Smart contract programming language" },
                new Skill { Id = 143, Name = "IoT", Category = "Emerging Tech", Description = "Internet of Things development" },
                new Skill { Id = 144, Name = "AR/VR", Category = "Emerging Tech", Description = "Augmented and Virtual Reality" },
                new Skill { Id = 145, Name = "Quantum Computing", Category = "Emerging Tech", Description = "Quantum-mechanical computing" },
                new Skill { Id = 146, Name = "Edge Computing", Category = "Emerging Tech", Description = "Distributed computing paradigm" },
                new Skill { Id = 147, Name = "Microservices", Category = "Architecture", Description = "Distributed system architecture" },
                new Skill { Id = 148, Name = "GraphQL", Category = "API", Description = "Query language for APIs" },
                new Skill { Id = 149, Name = "REST API", Category = "API", Description = "Representational State Transfer" },
                new Skill { Id = 150, Name = "WebSockets", Category = "API", Description = "Real-time communication protocol" }
            };

            modelBuilder.Entity<Skill>().HasData(skills);
        }
    }
}