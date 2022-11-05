using System.ComponentModel.DataAnnotations;

namespace Beca.AlbumInfo.API.Models
{
    public class AlbumForCreationDto
    {
        [Required(ErrorMessage = "Deberías proporcionar un título.")]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? Description { get; set; }
    }
}
