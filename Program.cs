using ColegioSanJose.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar controladores y vistas
builder.Services.AddControllersWithViews();

// --- EF Core con MySQL (Pomelo) ---
var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(cs, ServerVersion.AutoDetect(cs)));

var app = builder.Build();

// ---- SEED DATA ----
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Ejecutar el seeder (usa await si tu entorno lo permite)
    DbSeeder.SeedAsync(ctx).GetAwaiter().GetResult();
}
// --------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
