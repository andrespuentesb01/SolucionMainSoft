using AutoMapper;
using SlnMain.Domain;
using SlnMain.Domain.Models;

namespace SlnMain.Api
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {

            CreateMap<User, UsuarioDto>();
            CreateMap<UsuarioDto, User>();
        }
    }
}
