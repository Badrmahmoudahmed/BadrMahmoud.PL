using BadrMahmoud.PL.Helpers;
using BadrMahmoud.PL.Services.EmailSender;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositries;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;


namespace BadrMahmoud.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webapplicationbuilder = WebApplication.CreateBuilder(args);

			#region ConfigureServices
			webapplicationbuilder.Services.AddControllersWithViews();
			webapplicationbuilder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(webapplicationbuilder.Configuration.GetConnectionString("DefaultConnection")));
			//services.AddScoped<IDepartmentRepositries, DepartmentRepositries>();
			//services.AddScoped<IEmployeeReposititry , EmployeeRepositry>();
			webapplicationbuilder.Services.AddTransient<IEmailSender, EmailSender>();
			webapplicationbuilder.Services.AddScoped<IUnitofWork, UnitofWork>();
			webapplicationbuilder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
			webapplicationbuilder.Services.AddAutoMapper(M => M.AddProfile(new DeptMappProfile()));
			webapplicationbuilder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();
			webapplicationbuilder.Services.ConfigureApplicationCookie(o =>
			{
				o.LoginPath = "/Account/SignIn";
				o.ExpireTimeSpan = TimeSpan.FromDays(1);
				o.AccessDeniedPath = "/Home/Error";
			}


			); 
			#endregion

			var app = webapplicationbuilder.Build();

			#region Configure Kestrel
			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
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
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			}); 
			#endregion


			app.Run();
		}

       
    }
}
