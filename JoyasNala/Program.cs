using JoyasNala.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// Inicializar la base de datos con un usuario administrador
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

    if (!context.Usuarios.Any(u => u.NombreUsuario == "Nala"))
    {
        var usuarioAdmin = new Usuario
        {
            NombreUsuario = "Nala",
            Contrasena = "123456abd",
            EsAdministrador = true
        };
        context.Usuarios.Add(usuarioAdmin);
        context.SaveChanges();
    }

    // Añadir productos de ejemplo si no existen
    if (!context.Productos.Any())
    {
        var productos = new List<Producto>
        {
            new Producto
            {
                Nombre = "Collar de Perlas",
                Descripcion = "Hermoso collar de perlas blancas.",
                Precio = 15000m,
                Imagen = "/images/collar.jpg",
                OtrasImagenes = new List<string> { "/images/collar-1.jpg", "/images/collar-2.jpg" }
            },
            new Producto
            {
                Nombre = "Pulsera de Oro",
                Descripcion = "Elegante pulsera de oro 18k.",
                Precio = 20000m,
                Imagen = "/images/pulsera.jpg",
                OtrasImagenes = new List<string> { "/images/pulsera-1.jpg", "/images/pulsera-2.jpg" }
            },
            new Producto
            {
                Nombre = "Anillo de Plata",
                Descripcion = "Anillo de plata con incrustaciones de diamantes.",
                Precio = 25000m,
                Imagen = "/images/anillo.jpg",
                OtrasImagenes = new List<string> { "/images/anillo-1.jpg", "/images/anillo-2.jpg" }
            }
        };
        context.Productos.AddRange(productos);
        context.SaveChanges();
    }
}

// Redirigir a la página de catálogo al inicio
app.MapGet("/", async context =>
{
    context.Response.Redirect("/Catalogo");
    await Task.CompletedTask;
});

app.Run();
