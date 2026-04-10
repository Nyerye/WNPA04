/// <file>
/// Program.cs
/// </file>
/// <project>
/// Windows Network Programming Assignment 4
/// </project>
/// <author>
/// Nicholas Reilly
/// </author>
/// <date>
/// April 9 2026
/// </date>
/// <description>
/// Main file that builds and runs the application.
/// </description>
/// <references>
/// Deitel, P., & Deitel, H. (2017). *C# 6 for Programmers Sixth Edition* 
/// (Sixth, Ser. Deitel Development Series). Pearson Education.
/// </references>
/// 

using InsurancePal.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//Build in the services for cookie authentication, which will allow for the use of cookies to manage user authentication and authorization in the application.
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/Login";
        options.Cookie.Name = "InsurancePalAuth";

        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;

        
        options.Cookie.IsEssential = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

        //Make the cookie only last the session and have a 5 min timeout
        options.Cookie.MaxAge = null; 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5); 
    });


//Challenge all Authorize blocks unless I wrote AllowAnonymous. This al
builder.Services.AddAuthorization(options =>
{
    //Policy that challenges anyone trying to access resources behind the Login page
    options.FallbackPolicy = options.DefaultPolicy;

    //Policy that challenges a user trying to access resources open only to Admins.
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("IsAdmin", "true"));

});


//Give the application access to the ItemContext class. This allows the manipulation of the classes.
builder.Services.AddDbContext<ItemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Give the application access to the UserContext class. This allows for creation of users in the database.
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//Set the default route for the app when the user logs in.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();