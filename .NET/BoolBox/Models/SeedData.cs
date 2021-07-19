using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BoolBox.Data;

namespace BoolBox.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BoolBoxContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BoolBoxContext>>()))
            {
                // Look for any movies.
                if (context.Bool.Any())
                {
                    return;   // DB has been seeded
                }

                context.Bool.AddRange(
                    new Bool
                    {
                        Title = "SPM & Smash",
                        Date = DateTime.Parse("2021-7-14"),
                        Type = "Smash",
                        Duration = 7,
                        SpotifyID = "https://open.spotify.com/playlist/37i9dQZF1DZ06evO2xeauQ?si=a3e2843d98054237",
                        Repeat = "N"
                    },

                    new Bool
                    {
                        Title = "VR Bool",
                        Date = DateTime.Parse("2021-7-16"),
                        Type = "VR",
                        Duration = 4,
                        SpotifyID = "https://open.spotify.com/playlist/1f2oLiMhyPup7SU9PRCJb4?si=4361cf2ca52b4811",
                        Repeat = "Y"
                    },

                    new Bool
                    {
                        Title = "Return of the King",
                        Date = DateTime.Parse("2021-7-20"),
                        Type = "SOMD",
                        Duration = 7,
                        SpotifyID = "https://open.spotify.com/playlist/1vKC2r2e5an5gU3cl5H5wA?si=2952b7e3cecd43f1",
                        Repeat = "N"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
