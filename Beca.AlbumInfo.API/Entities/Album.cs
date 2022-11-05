using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beca.AlbumInfo.API.Entities
{
    public class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        public ICollection<Cancion> Canciones { get; set; }
               = new List<Cancion>();

        public Album(string title)
        {
            Title = title;
        }
    }
}
