using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dtos.Character;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetCharacters();
       Task<ServiceResponse<GetCharacterDto>> GetCharacter(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter([FromBody] AddCharacterDto newCharacter);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter([FromBody] UpdateCharacterDto updateCharacter);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);


    }
}