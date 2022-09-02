using Lms.Core.Repositories;
using Lms.Data.Data;
using Lms.Data.Data.Repositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lms.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var uow = serviceProvider.GetRequiredService<IUoW>();

                //uow.EnsureDeleted();
                //uow.Migrate();

                try
                {
                    await SeedData.InitAsync(uow);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return app;
        }
    }
}
