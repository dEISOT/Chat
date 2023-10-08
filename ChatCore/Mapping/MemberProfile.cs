using AutoMapper;
using ChatCore.DTO;
using ChatData.Entities;

namespace ChatCore.Mapping
{
    public class MemberProfile : Profile
    {
        public MemberProfile() 
        {
            CreateMap<Member, MemberDTO>().ReverseMap();

        }
    }
}
