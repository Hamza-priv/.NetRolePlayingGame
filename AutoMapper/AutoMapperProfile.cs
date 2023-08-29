using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dtos.Character;
using Model;

namespace AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();
        }
    }
}