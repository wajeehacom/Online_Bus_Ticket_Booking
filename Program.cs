using Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Regstration.Data;
using Regstration.Models;
using System.Security.Claims;
using Domain; // Ensure this namespace is correctly referenced
using Domain.Interface;
using Infrastructure1;

using Application.Services;
using Domain.Entities;
using Infrastructure1.Repositories;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Regstration.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDefaultIdentity<myAppUser>(options => options.SignIn.RequireConfirmedAccount = true)

    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(options => {    


    options.AddPolicy("BookingPolicy", policy =>
        policy.RequireAuthenticatedUser());

    
    options.AddPolicy("PakistaniPolicy", policy =>
      policy.RequireClaim(ClaimTypes.Country, "Pakistan"));
    options.AddPolicy("adminonly", policy =>
     policy.RequireClaim("firstname", "admin"));
     
});

//builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<IRepository<Buses>, GenericRepository<Buses>>();
//builder.Services.AddScoped<IRepository<BusSeats>, GenericRepository<BusSeats>>();
//builder.Services.AddScoped<IRepository<Booking>, GenericRepository<Booking>>();


//builder.Services.AddScoped<IBusRepository, BusRepository>();
//builder.Services.AddScoped<ISeatsRepository, SeatsRepository>();
//builder.Services.AddScoped<BookingService>();
//builder.Services.AddScoped<SeatsService>();
//builder.Services.AddScoped<BusService>();












// Register services




builder.Services.AddTransient<IRepository<Booking>>(provider => new GenericRepository<Booking>(connectionString));

builder.Services.AddTransient<IRepository<BusSeats>>(provider => new GenericRepository<BusSeats>(connectionString));
builder.Services.AddTransient<IRepository<Buses>>(provider => new GenericRepository<Buses>(connectionString));



//builder.Services.AddScoped<ISeatsRepository, SeatsRepository>(provider => new SeatsRepository(connectionString));
//builder.Services.AddScoped<IBusRepository, BusRepository>(provider => new BusRepository(connectionString));

builder.Services.AddScoped<ISeatsRepository>(provider =>
    new SeatsRepository(provider.GetRequiredService<IRepository<BusSeats>>(), connectionString));
builder.Services.AddScoped<IBusRepository>(provider =>
    new BusRepository(provider.GetRequiredService<IRepository<Buses>>(), connectionString));




builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<SeatsService>();
builder.Services.AddScoped<BusService>();


////again
////builder.Services.AddScoped<IRepository<Booking>, GenericRepository<Booking>>(provider => new GenericRepository<Booking>(connectionString));
////builder.Services.AddScoped<IRepository<BusSeats>, GenericRepository<BusSeats>>(provider => new GenericRepository<BusSeats>(connectionString));
////builder.Services.AddScoped<IRepository<Buses>, GenericRepository<Buses>>(provider => new GenericRepository<Buses>(connectionString));

////// Register custom repositories if they exist
////builder.Services.AddScoped<IBookingRepository, BookingRepository>(provider => new BookingRepository(connectionString));
////builder.Services.AddScoped<ISeatsRepository, SeatsRepository>(provider => new SeatsRepository(connectionString));
////builder.Services.AddScoped<IBusRepository, BusRepository>(provider => new BusRepository(connectionString));



////// Register services
////builder.Services.AddScoped<BookingService>();
////builder.Services.AddScoped<SeatsService>();
////builder.Services.AddScoped<BusService>();

builder.Services.AddSignalR();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapHub<SeatHub>("/SeatHub");


app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<SeatHub>("/SeatHub");
});


app.Run();
