using Microsoft.AspNetCore.Authorization;

namespace SO73653030
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, SampleAuthorizationMiddlewareResultHandler>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication("cookie")
                .AddCookie("cookie");

            builder.Services.AddAuthorization(config =>
            {
                //This should be the Tiers/Editions
                config.AddPolicy("BasicEdition", policyBuilder =>
                {
                    policyBuilder.UserRequireCustomClaim("BasicEdition");
                });

                config.AddPolicy("AdvancedEdition", policyBuilder =>
                {
                    policyBuilder.UserRequireCustomClaim("AdvancedEdition");
                });

                config.AddPolicy("PremiumEdition", policyBuilder =>
                {
                    policyBuilder.UserRequireCustomClaim("PremiumEdition");
                });
            });

            builder.Services.AddScoped<IAuthorizationHandler, PoliciesAuthorizationHandler>();
            builder.Services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}