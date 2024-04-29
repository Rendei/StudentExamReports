using Microsoft.AspNetCore.Identity;
using UTMNStudentsExamAnalysis;
using UTMNStudentsExamAnalysis.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StudentExamResultsContext>();
builder.Services.AddDbContext<IdentityContext>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:44476")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();   
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
