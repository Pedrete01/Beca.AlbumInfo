using Beca.AlbumInfo.API.Models;

namespace Beca.AlbumInfo.API
{
    public class AlbumesDataStore
    {
        public List<AlbumDto> Albumes { get; set; }
        
        //public static AlbumesDataStore Current { get; } = new AlbumesDataStore();

        public AlbumesDataStore()
        {
            // init dummy data
            Albumes = new List<AlbumDto>()
            {
                new AlbumDto()
                {
                     Id = 1,
                     Title = "Evolve",
                     Description = "Album de Imagine Dragons.",
                     Canciones = new List<CancionDto>()
                     {
                         new CancionDto() {
                             Id = 1,
                             Title = "Believer",
                             Description = "Pain!\r\nYou made me a, you made me a believer, believer\r\nPain!\r\nYou break me down, and build me up, believer, believer" },
                         new CancionDto() {
                             Id = 2,
                             Title = "Whatever it takes",
                             Description = "Whatever it takes\r\n'Cause I love the adrenaline in my veins\r\nI do whatever it takes\r\n'Cause I love how it feels when I break the chains\r\nWhatever it takes" },
                     }
                },
                new AlbumDto()
                {
                    Id = 2,
                    Title = "OK Orchestra",
                    Description = "Album de AJR.",
                    Canciones = new List<CancionDto>()
                     {
                         new CancionDto() {
                             Id = 3,
                             Title = "Bang!",
                             Description = "So put your best face on, everybody\r\nPretend you know this song, everybody\r\nCome hang\r\nLet's go out with a bang (bang! Bang! Bang!)" },
                          new CancionDto() {
                             Id = 4,
                             Title = "Joe",
                             Description = "It took a little while, but I found love (found love)\r\nI thought you'd reply, you just thumbed up (thumbed up)\r\nI play a lot of shows but you don't come (don't come)\r\nI don't even mind, this is so dumb, so dumb" },
                     }
                },
                new AlbumDto()
                {
                    Id= 3,
                    Title = "Surface sounds",
                    Description = "Album de Kaleo.",
                    Canciones = new List<CancionDto>()
                     {
                         new CancionDto() {
                             Id = 5,
                             Title = "Break my baby",
                             Description = "I want to break my baby\r\nYou know she loves to fake it\r\nI want to break my baby\r\nYeah, hold her down, bring her down now" },
                         new CancionDto() {
                             Id = 6,
                             Title = "Skinny",
                             Description = "You've got to stay skinny, don't you, girl?\r\nYou've got to stay pretty while you can\r\nYou've got to stay hungry for the fans\r\nOr they'll try to burn you all out\r\nThey'll try to burn you all out, yeah" },
                     }
                }
            };

        }

    }
}
