using AutoMapper;
using Beca.AlbumInfo.API;
using Beca.AlbumInfo.API.Entities;
using Beca.AlbumInfo.API.Models;
using Beca.AlbumInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Text.Json;


namespace AlbumInfo.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/albumes")]
    public class AlbumesController : ControllerBase
    {
        private readonly IAlbumInfoRepository _albumInfoRepository;
        private readonly IMapper _mapper;
        const int maxAlbumesPageSize = 20;

        public AlbumesController(IAlbumInfoRepository albumInfoRepository,
            IMapper mapper)
        {
            _albumInfoRepository = albumInfoRepository ?? 
                throw new ArgumentNullException(nameof(albumInfoRepository));
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumWithoutCancionesDto>>> GetAlbumes(
            string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxAlbumesPageSize)
            {
                pageSize = maxAlbumesPageSize;
            }

            var (albumEntities, paginationMetadata) = await _albumInfoRepository
                .GetAlbumesAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<AlbumWithoutCancionesDto>>(albumEntities));       
        }

        /// <summary>
        /// Get a city by id
        /// </summary>
        /// <param name="id">The id of the city to get</param>
        /// <param name="includeCanciones">Whether or not to include canciones</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns the requested album</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAlbum(
            int id, bool includeCanciones = false)
        {
            var album = await _albumInfoRepository.GetAlbumAsync(id, includeCanciones);
            if (album == null)
            {
                return NotFound();
            }

            if (includeCanciones)
            {
                return Ok(_mapper.Map<AlbumDto>(album));
            }

            return Ok(_mapper.Map<AlbumWithoutCancionesDto>(album));
        }

        [HttpPost]
        public async Task<ActionResult<AlbumDto>> CreateAlbum([FromBody] AlbumForCreationDto album)
        {
            AlbumForCreationDto albumCreado = new AlbumForCreationDto()
            {
                Title = album.Title,
                Description = album.Description
            };

            var finalAlbum = _mapper.Map<Album>(albumCreado);

            await _albumInfoRepository.AddAlbumAsync(finalAlbum);

            await _albumInfoRepository.SaveChangesAsync();

            return Ok(albumCreado);
        }

        [HttpPut("{albumid}")]
        public async Task<ActionResult> UpdateAlbum(int albumId, AlbumForUpdateDto album)
        {
            if (!await _albumInfoRepository.AlbumExistsAsync(albumId))
            {
                return NotFound();
            }

            var albumEntity = _albumInfoRepository.GetAlbumAsync(albumId, false).Result;

            _mapper.Map(album, albumEntity);

            await _albumInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{albumid}")]
        public async Task<ActionResult> PartiallyUpdateAlbum(
            int albumId,
            JsonPatchDocument<AlbumForUpdateDto> patchDocument)
        {
            if (!await _albumInfoRepository.AlbumExistsAsync(albumId))
            {
                return NotFound();
            }

            var albumEntity = _albumInfoRepository.GetAlbumAsync(albumId, false).Result;

            var albumToPatch = _mapper.Map<AlbumForUpdateDto>(albumEntity);

            patchDocument.ApplyTo(albumToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(albumToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(albumToPatch, albumEntity);
            await _albumInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{albumId}")]
        public async Task<ActionResult> DeleteAlbum(
            int albumId)
        {
            if (!await _albumInfoRepository.AlbumExistsAsync(albumId))
            {
                return NotFound();
            }

            var albumEntity = _albumInfoRepository.GetAlbumAsync(albumId, false).Result;

            _albumInfoRepository.DeleteAlbum(albumEntity);

            await _albumInfoRepository.SaveChangesAsync();

            return NoContent();
        }

    }
}
