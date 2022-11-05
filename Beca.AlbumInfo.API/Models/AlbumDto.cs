namespace Beca.AlbumInfo.API.Models
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int NumberOfCanciones
        {
            get
            {
                return Canciones.Count;
            }
        }

        public ICollection<CancionDto> Canciones { get; set; }
            = new List<CancionDto>();
    }
}