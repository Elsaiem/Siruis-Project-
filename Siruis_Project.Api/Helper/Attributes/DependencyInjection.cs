using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Siruis_Project.Core;
using Siruis_Project.Core.Entities.Identity;
using Siruis_Project.Core.RepositoryContract;
using Siruis_Project.Core.ServiceContract;
using Siruis_Project.Core.ServiceContract.IdentityServices;
using Siruis_Project.Repository.Data.Contexts;
using Siruis_Project.Repository.Identity.Context;
using Siruis_Project.Repository.Repositories;
using Siruis_Project.Service.Services.Clients;
using Siruis_Project.Service.Services.Contacts;
using Siruis_Project.Service.Services.Industries;
using Siruis_Project.Service.Services.Orders;
using Siruis_Project.Service.Services.Portofolios;
using Siruis_Project.Service.Services.Tasks;
using Siruis_Project.Service.Services.TeamMembers;
using Siruis_Project.Service.Services.Tokens;
using Siruis_Project.Service.Services.Users;
using System.Text;

namespace Siruis_Project.Api.Helper.Attributes
{







    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddBuildService();
            services.AddIdentityService();
            services.AddSwaggerdService();
            services.AddDataBaseCOnnectiondService(configuration);
            services.AddUSerDEfinedCOnnectiondService();
           services.AddAuthenticationService(configuration);

            return services;




        }
        private static IServiceCollection AddBuildService(this IServiceCollection services)
        {


            services.AddControllers();
            services.AddAutoMapper(typeof(Program)); // Adjust if needed to the appropriate startup class
            return services;


           
        }

        private static IServiceCollection AddIdentityService(this IServiceCollection services)
        {


            services.AddIdentity<AppUser, IdentityRole>()
                     .AddEntityFrameworkStores<SiruisIdentityDbContext>();




            return services;
        }
        private static IServiceCollection AddSwaggerdService(this IServiceCollection services)
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();



            return services;
        }
        private static IServiceCollection AddDataBaseCOnnectiondService(this IServiceCollection services, IConfiguration configuration)
        {

            


            services.AddDbContext<SiruisDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
             services.AddDbContext<SiruisIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });



            return services;
        }
        private static IServiceCollection AddUSerDEfinedCOnnectiondService(this IServiceCollection services)
        {


          
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITeamMemberService, TeamMemberService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIndustryServices, IndustryService>();
            services.AddScoped<IPortofolioServices, PortofolioServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));




            return services;
        }
        private static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }
            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))

                };

            });




            return services;
        }

    }
}
