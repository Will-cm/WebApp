using Microsoft.EntityFrameworkCore; // add for api rest
using WebApp;
using WebApp.Data.Context; //add for api rest

var builder = WebApplication.CreateBuilder(args);

///add
builder.Services.AddServices(builder.Configuration);  //añadimos archivo de config dependencyInjec...
//
builder.Services.AddEndpointsApiExplorer();  //add for swagger
builder.Services.AddSwaggerGen();  //add swagger

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(); ////add2

var app = builder.Build();

app.UseSwagger();  //habilitar swagger
app.UseSwaggerUI(); //habilitar swagger
//app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "api")); //habilitar swagger

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
//1: login
app.MapPost("/login", async (string User, string pass, DBContext db) => await db.users.FirstOrDefaultAsync(m => m.nombre == User && m.password == pass));
//2: lista de pedidos por usuario
app.MapGet("/nsalida", async (string User, DBContext db) => await db.nota_salida.Where(p => p.cod_vendedor == User).ToListAsync());
//3: listar picking por nota_salida
app.MapGet("/picking", async (int id_nsalida, DBContext db) => await db.picking_nsalida.Where(p => p.id_nsalida == id_nsalida).ToListAsync());
//4: actualizar picking
app.MapGet("/picking_update{id}", async (int id, decimal cant_picking, string usuario, DBContext db) =>
{
    var pic = await db.picking_nsalida.FindAsync(id);
    if (pic is null) return Results.NotFound();

    if ((cant_picking) <= pic.cantidad)
    {
        pic.cant_picking = cant_picking;
        pic.usuario = usuario;
        if ((pic.cant_picking) == pic.cantidad){
            pic.estado = 1;
        } else {
            pic.estado = 2;
        }
        await db.SaveChangesAsync();
    }
    else
    {
        return Results.Text("CANTIDAD EXEDIDA");
    }    
    return Results.NoContent();
});
// end API
app.Run();
