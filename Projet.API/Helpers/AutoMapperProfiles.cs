using AutoMapper;
using Projet.API.Dto;
using Projet.API.Model;

namespace Projet.API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, User>();
        }
    }
}