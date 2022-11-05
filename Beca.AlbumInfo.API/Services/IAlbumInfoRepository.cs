using Beca.AlbumInfo.API.Entities;

namespace Beca.AlbumInfo.API.Services
{
    public interface IAlbumInfoRepository
    {
        Task<IEnumerable<Album>> GetAlbumesAsync();
        Task<(IEnumerable<Album>, PaginationMetadata)> GetAlbumesAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<Album?> GetAlbumAsync(int albumId, bool includeCanciones);
        Task<bool> AlbumExistsAsync(int albumId);
        Task<IEnumerable<Cancion>> GetCancionesForAlbumAsync(int albumId);
        Task<Cancion?> GetCancionForAlbumAsync(int albumId,
            int cancionId);
        Task AddCancionForAlbumAsync(int cityId, Cancion cancion);
        void DeleteCancion(Cancion cancion);
        Task<bool> AlbumNameMatchesAlbumId(string? albumName, int albumId);
        Task<bool> SaveChangesAsync();

        Task AddAlbumAsync(Album album);

        void DeleteAlbum(Album album);
    }
}
