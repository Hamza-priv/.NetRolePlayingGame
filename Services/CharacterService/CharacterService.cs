using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data;
using Dtos.Character;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character{Id=1,Name = "Boa"},
            new Character{Id=2,Name = "Cool" }
        };


        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter([FromBody] AddCharacterDto newCharacter)
        {
            // service respone with a list of character
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            // when have to convert a whole list
            serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var reqCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            if (reqCharacter is not null)
            {
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(reqCharacter);
                return serviceResponse;
            }
            throw new Exception("No character found");
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        // public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter([FromBody] UpdateCharacterDto updateCharacter)
        // {
        //     var serviceResponse = new ServiceResponse<GetCharacterDto>();
        //     var character = characters.FirstOrDefault(c => c.Id == updateCharacter.Id);

        //     if (character == null)
        //     {
        //         serviceResponse.Success = false;
        //         serviceResponse.Message = "Character not found.";
        //         return serviceResponse;
        //     }

        //     // Use AutoMapper to update the character properties from updateCharacter
        //     _mapper.Map(updateCharacter, character);

        //     serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
        //     return serviceResponse;
        // }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter([FromBody] UpdateCharacterDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
                if (character is null)
                    throw new Exception($"Character with Id '{updateCharacter.Id}' not found.");

                character.Name = updateCharacter.Name;
                character.Hitpoints = updateCharacter.Hitpoints;
                character.Strength = updateCharacter.Strength;
                character.Defense = updateCharacter.Defense;
                character.Intelligence = updateCharacter.Intelligence;
                character.Class = updateCharacter.Class;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            try
            {
                if (character is null)
                    throw new Exception($"Character with Id '{id}' not found.");

                _context.Characters.Remove(character);

                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}