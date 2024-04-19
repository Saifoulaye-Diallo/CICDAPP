
using Microsoft.EntityFrameworkCore;
using SeleniumApp.Data;
using SeleniumApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<SeleniumAppDbContext>(options => options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SeleniumAppDB;Trusted_Connection=True;"));

// Configure in memory database
builder.Services.AddDbContext<SeleniumAppDbContext>(opt => opt.UseInMemoryDatabase("SeleniumAppDB"));

builder.Services.AddScoped<SeleniumAppDbContext>();



var app = builder.Build();

// 2. Find the service within the scope to use
using (var scope = app.Services.CreateScope())
{
    // 3. Get the instance of SeleniumAppDbContext in our service layer
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SeleniumAppDbContext>();

    // 4. Call the SeedDataGenerator to generate seed data
    SeedDataGenerator.Initialize(services);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Etudiants}/{action=Index}/{id?}");

app.Run();
