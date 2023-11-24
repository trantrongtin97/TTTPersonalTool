using AutoMapper;
using TTT.PersonalTool.Shared.Dtos;
using TTT.PersonalTool.Shared.Models;

namespace TTT.PersonalTool.Server
{
    public class PersonalToolProfile : Profile
    {
        public PersonalToolProfile()
        {
            CreateMap<RegisterDto, User>();
        }
    }
}
