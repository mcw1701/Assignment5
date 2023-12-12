using Assignment5.Data;
using Microsoft.EntityFrameworkCore;

namespace Assignment5.Models
{
    public static class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Assignment5Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<Assignment5Context>>()))
            {
                // Look for any movies.
                if (context.Song.Any())
                {
                    return;   // DB has been seeded
                }
                context.Song.AddRange(
                    new Song
                    {
                        Title = "Stronger",
                        Genre = "Rap",
                        Performer = "Kanye West",
                        Price = 7.99M
                    },
                    new Song
                    {
                        Title = "Where Is My Mind",
                        Genre = "Alternative",
                        Performer = "The Pixies",
                        Price = 8.99M
                    },
                    new Song
                    {
                        Title = "My Hero",
                        Genre = "Rock",
                        Performer = "Foo Fighters",
                        Price = 9.99M
                    },
                    new Song
                    {
                        Title = "So What",
                        Genre = "Jazz",
                        Performer = "Miles Davis",
                        Price = 3.99M
                    }
                );
                context.SaveChanges();
            }
        }

    }
}
