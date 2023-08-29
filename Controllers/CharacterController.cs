using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dtos.Character;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services.CharacterService;

namespace Controllers
{
    [ApiController]
    [Route("api/character")]
    public class CharacterContoller : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterContoller(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("/characters")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetAll()
        {
              return Ok(await _characterService.GetCharacters());
          
        }

        [HttpGet("/character/{id:int}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetCharacter(int id)
        {
            return Ok(await _characterService.GetCharacter(id));
        }

        [HttpPost("/addCharacter")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharcter([FromBody] AddCharacterDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut("/updateCharacter")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter([FromBody] UpdateCharacterDto updateCharacter)
        {
            return Ok(await _characterService.UpdateCharacter(updateCharacter));
        }

        [HttpDelete("/deleteCharacter/{id:int}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharcter(int id)
        {
            return Ok(await _characterService.DeleteCharacter(id));
        }
    }
}