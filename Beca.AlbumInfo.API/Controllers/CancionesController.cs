using AutoMapper;
using Beca.AlbumInfo.API.Models;
using Beca.AlbumInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Beca.AlbumInfo.API.Controllers
{
    [Route("api/albumes/{albumId}/canciones")]
    [Authorize(Policy = "MustBeFromEvolve")]
    [ApiController]
    public class CancionesController : ControllerBase
    {
        private readonly ILogger<CancionesController> _logger;
        private readonly IAlbumInfoRepository _albumInfoRepository;
        private readonly IMapper _mapper;

        public CancionesController(ILogger<CancionesController> logger,
            IAlbumInfoRepository albumInfoRepository,
            IMapper mapper)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _albumInfoRepository = albumInfoRepository ??
                throw new ArgumentNullException(nameof(albumInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CancionDto>>> GetCanciones(
            int albumId)
        {

            //var cityName = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value;

            //if (!await _cityInfoRepository.CityNameMatchesCityId(cityName, cityId))
            //{
            //    return Forbid();
            //}

            if (!await _albumInfoRepository.AlbumExistsAsync(albumId))
            {
                _logger.LogInformation(
                    $"El album con la id {albumId} no ha sido encontrado.");
                return NotFound();
            }

            var cancionesForAlbum = await _albumInfoRepository
                .GetCancionesForAlbumAsync(albumId);

            return Ok(_mapper.Map<IEnumerable<CancionDto>>(cancionesForAlbum));
        }

        [HttpGet("{cancionid}", Name = "GetCancion")]
        public async Task<ActionResult<CancionDto>> GetCancion(
            int albumId, int cancionId)
        {
            if (!await _albumInfoRepository.AlbumExistsAsync(albumId))
            {
                return NotFound();
            }

            var pointOfInterest = await _albumInfoRepository
                .GetCancionForAlbumAsync(albumId, cancionId);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CancionDto>(pointOfInterest));
        }

        [HttpPost]
        public async Task<ActionResult<CancionDto>> CreateCancion(
           int albumId,
           CancionForCreationDto cancion)
        {
            if (!await _albumInfoRepository.AlbumExistsAsync(albumId))
            {
                return NotFound();
            }

            var finalCancion = _mapper.Map<Entities.Cancion>(cancion);

            await _albumInfoRepository.AddCancionForAlbumAsync(
                albumId, finalCancion);

            await _albumInfoRepository.SaveChangesAsync();

            var createdCancionToReturn =
                _mapper.Map<Models.CancionDto>(finalCancion);

            return CreatedAtRoute("GetCancion",
                 new
                 {
                     albumId = albumId,
                     cancionId = createdCancionToReturn.Id
                 },
                 createdCancionToReturn);
        }

        [HttpPut("{cancionid}")]
        public async Task<ActionResult> UpdateCancion(int albumId, int cancionId,
            CancionForUpdateDto cancion)
        {
            if (!await _albumInfoRepository.AlbumExistsAsync(albumId))
            {
                return NotFound();
            }

            var cancionEntity = await _albumInfoRepository
                .GetCancionForAlbumAsync(albumId, cancionId);
            if (cancionEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(cancion, cancionEntity);

            await _albumInfoRepository.SaveChangesAsync();

            return NoContent();
        }


        [HttpPatch("{cancionid}")]
        public async Task<ActionResult> PartiallyUpdateCancion(
            int albumId, int cancionId,
            JsonPatchDocument<CancionForUpdateDto> patchDocument)
        {
            if (!await _albumInfoRepository.AlbumExistsAsync(albumId))
            {
                return NotFound();
            }

            var cancionEntity = await _albumInfoRepository
                .GetCancionForAlbumAsync(albumId, cancionId);
            if (cancionEntity == null)
            {
                return NotFound();
            }

            var cancionToPatch = _mapper.Map<CancionForUpdateDto>(
                cancionEntity);

            patchDocument.ApplyTo(cancionToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(cancionToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(cancionToPatch, cancionEntity);
            await _albumInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{cancionId}")]
        public async Task<ActionResult> DeleteCancion(
            int albumId, int cancionId)
        {
            if (!await _albumInfoRepository.AlbumExistsAsync(albumId))
            {
                return NotFound();
            }

            var cancionEntity = await _albumInfoRepository
                .GetCancionForAlbumAsync(albumId, cancionId);
            if (cancionEntity == null)
            {
                return NotFound();
            }

            _albumInfoRepository.DeleteCancion(cancionEntity);
            await _albumInfoRepository.SaveChangesAsync();

            return NoContent();
        }

    }
}
