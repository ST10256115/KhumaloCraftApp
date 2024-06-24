using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using KhumaloCraft.Data;
using KhumaloCraft.Services;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.DurableTask.ContextImplementations;
using KhumaloClientFactory = KhumaloCraft.Services.DurableClientFactory;



var builder = WebApplication.CreateBuilder(args);

// Register DurableTaskOptions from appsettings.json
builder.Services.Configure<DurableTaskOptions>(builder.Configuration.GetSection("DurableTaskOptions"));

// Add DurableClientFactory
builder.Services.AddSingleton<IDurableClientFactory, KhumaloClientFactory>();


// Add OrderService
builder.Services.AddTransient<OrderService>();

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<KhumaloCraftAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));





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
