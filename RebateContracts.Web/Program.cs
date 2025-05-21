using Microsoft.EntityFrameworkCore;
using RebateContracts.Infrastructure;
using RebateContracts.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register business logic services for DI using Application layer extension
builder.Services.AddRebateCalculationServices();

// Configure EF Core DbContext
builder.Services.AddDbContext<RebateContractsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Seed CSV data at startup (dev/ops only)
if (args.Contains("--seed-csv"))
{
    using var scope = app.Services.CreateScope();
    await DbInitializer.SeedFromCsvAsync(scope.ServiceProvider, Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "datas"));
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
