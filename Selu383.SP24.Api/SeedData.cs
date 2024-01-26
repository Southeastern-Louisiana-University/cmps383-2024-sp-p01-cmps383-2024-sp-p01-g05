using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;


namespace Selu383.SP24.Api
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DataContext>>()))
            {
                // Look for any movies.
                if (context.Hotels.Any())
                {
                    return;   // DB has been seeded
                }
                context.Hotels.AddRange(
                    new Hotel
                    {
                        Name = "Hotel One",
                        Address = "Address One",
                    },
                    new Hotel
                    {
                        Name = "Hotel Two",
                        Address = "Address Two",
                    },
                    new Hotel
                    {
                        Name = "Hotel Three",
                        Address = "Address Three",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
