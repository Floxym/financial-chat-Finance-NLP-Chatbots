using AutoMapper;
using Chat.Domain.Entities;
using Chat.Domain.Models;

namespace Chat.Domain.AutoMapperMappings
{
    public class ChatRoomMappingProfile : Profile
    {
        public ChatRoomMappingProfile()
        {
            CreateMap<Room, RoomModel>().ReverseMap();
        }
    }
}
