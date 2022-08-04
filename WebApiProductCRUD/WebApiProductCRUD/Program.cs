using Microsoft.EntityFrameworkCore;
using WebApiProductCRUD.Data.Configuration;
using WebApiProductCRUD.Data.Context;
using Microsoft.AspNetCore.Identity;
using WebApiProductCRUD.Models;
using AutoMapper;
using WebApiProductCRUD.Data.AutoMapper;
using WebApiProductCRUD.Data;
using WebApiProductCRUD.Models.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebApiProductCRUD.Services;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.AddControllersWithViews();

var services = builder.Services;
services.AddRazorPages();

services.ConfigureDbContext(configuration);
services.ConfigureUserAndIdentity();
services.ConfigureServices();
services.ConfigureRepositories();
services.ConfigureAutoMapper();

services.AddTransient<IEmailSender>(x =>
    new BasicMailSender("smtp.gmail.com", "[mail]@gmail.com", "password"));

var app = builder.Build();
await RunSeeding(app, args);

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

//app.UseSession();
//app.Use(async (context, next) =>
//{
//    var token = context.Session.GetString("Token");
//    if (!string.IsNullOrEmpty(token))
//    {
//        context.Request.Headers.Add("Authorization", "Bearer " + token);
//    }
//    await next();
//});

app.UseStaticFiles();

app.UseMiddleware<JwtMiddleware>();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "area",
        pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}");
});

app.UseEndpoints(enpoints =>
{
    enpoints.MapGet("/", async httpCtx =>
    {
        await Task.Yield();
        httpCtx.Response.Redirect($"/Web");
    });
});

app.MapRazorPages();

app.Urls.Add("http://*:1020");
app.Urls.Add("https://*:2020");
app.Run();

static async Task RunSeeding(IHost host, string[] args)
{
    var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
    using IServiceScope? scope = scopeFactory?.CreateScope();
    var seeder = scope?.ServiceProvider.GetService<DbSeeder>();
    if (seeder is not null)
        await seeder.SeedAsync(args);
}