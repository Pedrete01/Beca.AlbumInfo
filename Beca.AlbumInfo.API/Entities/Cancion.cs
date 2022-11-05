using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beca.AlbumInfo.API.Entities
{
    public class Cancion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }


        [ForeignKey("AlbumId")]
        public Album? Album { get; set; }
        public int AlbumId { get; set; }

        public Cancion(string title)
        {
            Title = title;
        }
    }
}
