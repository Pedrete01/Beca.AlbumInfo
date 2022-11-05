using Beca.AlbumInfo.API.Services;
using Beca.AlbumInfo.API.DbContexts;
using Beca.AlbumInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beca.AlbumInfo.API.Services
{
    public class AlbumInfoRepository : IAlbumInfoRepository
    {
        private readonly AlbumInfoContext _context;

        public AlbumInfoRepository(AlbumInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Album>> GetAlbumesAsync()
        {
            return await _context.Albumes.OrderBy(c => c.Title).ToListAsync();
        }

        public async Task<bool> AlbumNameMatchesAlbumId(string? albumName, int albumId)
        {
            return await _context.Albumes.AnyAsync(c => c.Id == albumId && c.Title == albumName);
        }

        public async Task<(IEnumerable<Album>, PaginationMetadata)> GetAlbumesAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            // collection to start from
            var collection = _context.Albumes as IQueryable<Album>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Title == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Title.Contains(searchQuery)
                    || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(c => c.Title)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }



        public async Task<Album?> GetAlbumAsync(int albumId, bool includeCanciones)
        {
            if (includeCanciones)
            {
                return await _context.Albumes.Include(c => c.Canciones)
                    .Where(c => c.Id == albumId).FirstOrDefaultAsync();
            }

            return await _context.Albumes
                  .Where(c => c.Id == albumId).FirstOrDefaultAsync();
        }

        public async Task<bool> AlbumExistsAsync(int albumId)
        {
            return await _context.Albumes.AnyAsync(c => c.Id == albumId);
        }

        public async Task<Cancion?> GetCancionForAlbumAsync(
            int albumId,
            int cancionId)
        {
            return await _context.Canciones
               .Where(p => p.AlbumId == albumId && p.Id == cancionId)
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Cancion>> GetCancionesForAlbumAsync(
            int albumId)
        {
            return await _context.Canciones
                           .Where(p => p.AlbumId == albumId).ToListAsync();
        }

        public async Task AddCancionForAlbumAsync(int albumId,
            Cancion cancion)
        {
            var album = await GetAlbumAsync(albumId, false);
            if (album != null)
            {
                album.Canciones.Add(cancion);
            }
        }

        public void DeleteCancion(Cancion cancion)
        {
            _context.Canciones.Remove(cancion);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task AddAlbumAsync(Album album)
        {
            if (album != null)
            {
                _context.Albumes.Add(album);
            }
        }

        public async void DeleteAlbum(Album album)
        {
            var listaCanciones = await _context.Canciones.Where(p => p.AlbumId == album.Id).ToListAsync();

            foreach(var cancion in listaCanciones)
            {
                DeleteCancion(cancion);
            }

            _context.Albumes.Remove(album);
        }
    }
}
