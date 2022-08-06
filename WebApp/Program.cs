using Microsoft.EntityFrameworkCore; // add for api rest
using WebApp;
using WebApp.Data.Context; //add for api rest

var builder = WebApplication.CreateBuilder(args);

///add
builder.Services.AddServices(builder.Configuration);  //añadimos archivo de config dependencyInjec...
//

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(); ////add2

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseSession();  ////add3

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller}/{action}/{id?}");

// API REST minima
app.MapGet("/categoria", async (DBContext db) => await db.categoria.ToListAsync());
// end API
app.Run();
