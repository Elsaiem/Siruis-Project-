
using Siruis_Project.Api.Helper.Attributes;

namespace Siruis_Project.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDependency(builder.Configuration);

            var app = builder.Build();

            await app.ConfigureMiddlewares();


        }
    }
}
