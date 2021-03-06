using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using asp_test.Models.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<asp_testContext>(options =>
       options.UseSqlServer(
            //appsetings.json から引数名の文字列を取得(appsettings.json の11行目の値)
           builder.Configuration.GetConnectionString("DefaultConnection")
           )
       );

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();