using AutoMapper;
using ChatCore.DTO;
using ChatData.Entities;

namespace ChatCore.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
