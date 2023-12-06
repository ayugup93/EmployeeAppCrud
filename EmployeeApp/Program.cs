using Employee.Infrastructure.DbEntities;
using Employee.Services.Interfaces;
using Employee.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<EmployeeDbContext>(
options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("EmployeesDBConnection"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.23-mariadb"));
});

builder.Host.UseSerilog((hostContext, services, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
});

builder.Services.AddTransient<IEmployeeService,EmployeeService>();
builder.Services.AddTransient<IEmployeeDapperService, EmployeeDapperService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeDb API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Enable middleware to serve generated Swagger as a JSON endpoint
app.UseSwagger();

// Enable middleware to serve Swagger UI (HTML, JS, CSS, etc.)
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using(var scope = app.Services.CreateScope())
{
    var factory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using var scopeF = factory.CreateScope();
    var context = scopeF.ServiceProvider.GetRequiredService<EmployeeDbContext>();
}

app.Run();
