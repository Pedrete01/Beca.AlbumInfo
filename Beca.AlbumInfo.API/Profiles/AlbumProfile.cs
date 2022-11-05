using AutoMapper;

namespace Beca.AlbumInfo.API.Profiles
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            CreateMap<Entities.Album, Models.AlbumWithoutCancionesDto>();
            CreateMap<Entities.Album, Models.AlbumDto>();
            CreateMap<Models.AlbumForCreationDto, Entities.Album>();
            CreateMap<Models.AlbumForUpdateDto, Entities.Album>();
            CreateMap<Entities.Album, Models.AlbumForUpdateDto>();
        }
    }
}