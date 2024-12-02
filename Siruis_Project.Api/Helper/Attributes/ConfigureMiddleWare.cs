using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Siruis_Project.Core.Entities.Identity;
using Siruis_Project.Repository.Identity;
using Siruis_Project.Repository.Identity.Context;

namespace Siruis_Project.Api.Helper.Attributes
{



    public static class ConfigureMiddleWare
    {

        public static async Task<WebApplication> ConfigureMiddlewares(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var contextIdentity = service.GetRequiredService<SiruisIdentityDbContext>();
            var UserManger = service.GetRequiredService<UserManager<AppUser>>();
            var looger = service.GetRequiredService<ILoggerFactory>();

            try
            {
               

                await contextIdentity.Database.MigrateAsync();
                await SiruisProjectDbContextSeed.SeedSiruisUserAsync(UserManger);

            }

            catch (Exception ex)
            {

                var loger = looger.CreateLogger<Program>();
                loger.LogError(ex, "there are problem during apply migrations !");

            }
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}");


            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();



            return app;





        }




    }
}
