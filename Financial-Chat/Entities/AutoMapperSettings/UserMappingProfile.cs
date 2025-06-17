using AutoMapper;
using Chat.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Chat.Domain.AutoMapperMappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<IdentityUser, UserModel>().ReverseMap();
        }
    }
}
