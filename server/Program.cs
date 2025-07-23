using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using SmartCareerPlatform; 
using SmartCareerPlatform.Services;
using System.Text;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using SmartCareerPlatform.Server.Data;
using SmartCareerPlatform.Config;
using SmartCareerPlatform.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? "your_super_secret_key_1234567890!@#$";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "your_app";

// Register JWT authentication
builder.Services.AddJwtAuthentication(jwtKey, jwtIssuer);

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseRecommendationService, CourseRecommendationService>();
builder.Services.AddScoped<CourseraApiService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IUserCourseInteractionRepository, UserCourseInteractionRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("AllowAll");

app.UseCors("AllowAll"); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.EnsureCreatedAsync();
    DbInitializer.Initialize(context);
}

await app.RunAsync();