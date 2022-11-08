using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Beca.AlbumInfo.API.DbContexts;
using Beca.AlbumInfo.API.Services;

namespace Albumes.Test
{
    public class AlbumesServiciosTest : IDisposable
    {
        private AlbumInfoRepository _albumInfoRepository;
        public AlbumesServiciosTest()
        {
            var connection = new SqliteConnection("Data source=:memory:");
            connection.Open();

            var optionsBuilder = new DbContextOptionsBuilder<AlbumInfoContext>().UseSqlite(connection);
            var _dbContext = new AlbumInfoContext(optionsBuilder.Options);

            _dbContext.Database.Migrate();

            _albumInfoRepository = new AlbumInfoRepository(_dbContext);
        }
        public void Dispose()
        {

        }

        [Fact]
        public async Task AlbumServicios_ComprobarTieneCanciones()
        {
            // Arrange
            var album = await _albumInfoRepository.GetAlbumAsync(1, false);

            // Act

            // Assert
            Assert.Empty(album.Canciones);
        }

        [Fact]
        public async Task AlbumServicios_ComprobarNumeroCanciones()
        {
            // Arrange
            var album = await _albumInfoRepository.GetAlbumAsync(1, false);

            // Act
            int numero = album.Canciones.Count;

            // Assert
            Assert.True(2 >= numero);
        }

        [Fact]
        public async Task AlbumServicios_ComprobarNombreAlbum()
        {
            // Arrange
            var album = await _albumInfoRepository.GetAlbumAsync(1, false);

            // Act

            // Assert
            Assert.Equal("Evolve", album.Title);
        }
    }

}
