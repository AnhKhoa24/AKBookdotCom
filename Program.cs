using AKBookdotCom.Areas.Admin.Contacts;
using AKBookdotCom.Areas.Admin.Services;
using AKBookdotCom.Contacts;
using AKBookdotCom.Models.Entities;
using AKBookdotCom.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Cấu hình dịch vụ Authentication với Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login"; // Đường dẫn trang đăng nhập
        options.LogoutPath = "/Auth/Logout"; // Đường dẫn trang đăng xuất
        options.AccessDeniedPath = "/Auth/AccessDenied"; // Trang từ chối truy cập
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Thời gian hết hạn cookie
        options.SlidingExpiration = true; // Gia hạn cookie mỗi lần người dùng hoạt động
    });

// Cấu hình Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});



builder.Services.AddDbContext<QuanLySachContext>(c =>
        c.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddScoped<ISachClient, SachClient>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IQuanLySach, QuanLySach>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Đăng ký route mặc định cho Client Area
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Client" });

// Đăng ký route cho Admin Area
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
