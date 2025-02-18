using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TipTrip.Application.Implements.Interfaces;
using TipTrip.Application.Implements.Services;
using TipTrip.Common.Models;
using TipTrip.IdentityFramework.DBContext;
using TipTrip.Implements.Interfaces;
using TipTrip.Implements.Services;
using TipTrip.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true; // Ensure email is unique
})
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

// Add application-specific services
builder.Services.AddScoped<IMeAuthenticationService, AuthenticationService>()
                .AddScoped<IOpenAIService, OpenAIService>()
                .AddScoped<ISuggestionService, SuggestionService>();

// Service Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Expiry time
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// IHttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<IEmailService, EmailService>();

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
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapGet("/", () => Results.Redirect("/Page/Homepage"));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Page}/{action=Homepage}");

app.MapRazorPages();
app.Run();
