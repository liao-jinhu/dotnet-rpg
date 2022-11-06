using dotnet_rpg.Dtos.Character;
using AutoMapper;
using dotnet_rpg.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterServices
    {
         private static List<Character> characters = new List<Character>(){
            new Character(),
            new Character{
                Id = 1,
                Name = "Sam"
            }
        };

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper,DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
             _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int userId)
        {
            // return new ServiceResponse<List<GetCharacterDto>>
            // {
            //     Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()
            // };
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters  = await _context.Characters
            .Where(c => c.User.Id == userId)
            .ToListAsync();
            response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetAllCharacterById(int id)
        {
            // var serviceResponse = new ServiceResponse<GetCharacterDto>();
            // var character = characters.FirstOrDefault(c => c.Id == id);
            // serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            // return serviceResponse;
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                //Character character = characters.FirstOrDefault(c => c.Id == updateCharacter.Id);
                var character = await _context.Characters.FirstOrDefaultAsync(c=>c.Id == updateCharacter.Id);

                _mapper.Map(updateCharacter, character);
                // character.Name = updateCharacter.Name;
                // character.HitPoints = updateCharacter.HitPoints;
                // character.Strength = updateCharacter.Strength;
                // character.Defense = updateCharacter.Defense;
                // character.Class = updateCharacter.Class;

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
             ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _context.Characters.FirstAsync(c => c.Id == id);
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                response.Data = _context.Characters.Select(c =>_mapper.Map<GetCharacterDto>(c)).ToList();
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }
    }
}