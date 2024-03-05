using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebToDoApp;
using WebToDoApp.Models;
using WebToDoApp.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<ToDoItemService>();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        var app = builder.Build();

        // Seed verileri eklemek için bir service scope oluþturuyoruz
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                // Veritabaný baðlantýsýný alýyoruz
                var dbContext = services.GetRequiredService<AppDbContext>();

                // Seed verileri eklemek için bir method çaðýrýyoruz
                SeedData.Initialize(dbContext);
            }
            catch (Exception ex)
            {
                // Seed verileri eklerken bir hata oluþursa burada iþleyebiliriz
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Seed data eklenirken bir hata oluþtu.");
            }
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
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
