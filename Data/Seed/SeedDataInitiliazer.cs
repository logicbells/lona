using System;
using Lona.Data.Context;
using Lona.Data.Entities;

namespace Lona.Data.Seed
{
    public static class SeedDataInitiliazer
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new LonaDbContext(serviceProvider.GetRequiredService<DbContextOptions<LonaDbContext>>());


            if (context == null || context.Loans == null)
            {
                throw new ArgumentNullException("Null LonaDbContext");
            }
            // Look for any movies.
            if (context.Loans.Any())
            {
                return;   // DB has been seeded
            }

            await context.Database.MigrateAsync();
        }
    }
}

