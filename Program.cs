using SPL_Diplom_Winki_Trippi_Sabi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SafetyFirstWebsite.Models;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

// Datenbank konfigurieren
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity hinzufügen (mit Rollen)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Razor Pages und MVC aktivieren
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Entwicklungs-Fehlermeldungen aktivieren
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Rollen & Admin-Benutzer automatisch anlegen
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!context.Services.Any())
    {
        context.Services.AddRange(
            new Service { Title = "Alarmanlagen", Description = "Moderne Funk-Alarmanlagen für effektiven Schutz von Wohn- und Geschäftsgebäuden.", ImageUrl = "/images/alarmanlage.jpg" },
            new Service { Title = "Videosysteme", Description = "Hochwertige Überwachungssysteme zur Abschreckung und Nachverfolgung.", ImageUrl = "/images/videosystem.jpg" },
            new Service { Title = "Gegensprechanlagen", Description = "Besucher sicher identifizieren, bevor Sie die Tür öffnen.", ImageUrl = "/images/gegensprechanlage.jpg" },
            new Service { Title = "Tresore", Description = "Zuverlässiger Diebstahlschutz mit geprüften Tresoren von Wertheim.", ImageUrl = "/images/tresor.jpg" }
        );
        context.SaveChanges();
    }


    // Rolle Admin erstellen (falls noch nicht vorhanden)
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Admin-Benutzer erstellen (falls noch nicht vorhanden)
    string adminEmail = "admin@safetyfirst.com";
    string adminPassword = "Admin123!";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var newAdmin = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(newAdmin, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
}




app.Run();
