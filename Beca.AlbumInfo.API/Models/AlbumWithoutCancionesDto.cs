namespace Beca.AlbumInfo.API.Models
{
    public class AlbumWithoutCancionesDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}