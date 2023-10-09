using AutoMapper;
using ChatCore.DTO;
using ChatData.Entities;

namespace ChatCore.Mapping.AutoMapper
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageDTO>().ReverseMap();
        }
    }
}
