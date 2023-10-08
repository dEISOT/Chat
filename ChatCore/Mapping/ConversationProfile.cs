using AutoMapper;
using ChatCore.DTO;
using ChatData.Entities;

namespace ChatCore.Mapping
{
    public class ConversationProfile : Profile
    {
        public ConversationProfile() 
        { 
            CreateMap<Conversation, ConversationDTO>().ReverseMap();
        }
    }
}
