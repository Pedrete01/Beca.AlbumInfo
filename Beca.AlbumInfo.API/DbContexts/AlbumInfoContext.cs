using Beca.AlbumInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beca.AlbumInfo.API.DbContexts
{
    public class AlbumInfoContext : DbContext
    {
        public DbSet<Album> Albumes { get; set; } = null!;
        public DbSet<Cancion> Canciones { get; set; } = null!;

        public AlbumInfoContext(DbContextOptions<AlbumInfoContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .HasData(
               new Album("Evolve")
               {
                   Id = 1,
                   Description = "Album de Imagine Dragons."
               },
               new Album("OK Orchestra")
               {
                   Id = 2,
                   Description = "Album de AJR."
               },
               new Album("Surface sounds")
               {
                   Id = 3,
                   Description = "Album de Kaleo."
               });

            modelBuilder.Entity<Cancion>()
             .HasData(
               new Cancion("Believer")
               {
                   Id = 1,
                   AlbumId = 1,
                   Description = "Pain!\r\nYou made me a, you made me a believer, believer\r\nPain!\r\nYou break me down, and build me up, believer, believer"
               },
               new Cancion("Whatever it takes")
               {
                   Id = 2,
                   AlbumId = 1,
                   Description = "Whatever it takes\r\n'Cause I love the adrenaline in my veins\r\nI do whatever it takes\r\n'Cause I love how it feels when I break the chains\r\nWhatever it takes"
               },
                 new Cancion("Bang!")
                 {
                     Id = 3,
                     AlbumId = 2,
                     Description = "So put your best face on, everybody\r\nPretend you know this song, everybody\r\nCome hang\r\nLet's go out with a bang (bang! Bang! Bang!)"
                 },
               new Cancion("Joe")
               {
                   Id = 4,
                   AlbumId = 2,
                   Description = "It took a little while, but I found love (found love)\r\nI thought you'd reply, you just thumbed up (thumbed up)\r\nI play a lot of shows but you don't come (don't come)\r\nI don't even mind, this is so dumb, so dumb"
               },
               new Cancion("Break my baby")
               {
                   Id = 5,
                   AlbumId = 3,
                   Description = "I want to break my baby\r\nYou know she loves to fake it\r\nI want to break my baby\r\nYeah, hold her down, bring her down now"
               },
               new Cancion("Skinny")
               {
                   Id = 6,
                   AlbumId = 3,
                   Description = "You've got to stay skinny, don't you, girl?\r\nYou've got to stay pretty while you can\r\nYou've got to stay hungry for the fans\r\nOr they'll try to burn you all out\r\nThey'll try to burn you all out, yeah"
               }
               );
            base.OnModelCreating(modelBuilder);
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("connectionstring");
            base.OnConfiguring(optionsBuilder);
        }*/
    }
}
