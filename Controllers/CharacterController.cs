using Microsoft.AspNetCore.Mvc;
using dotnet_rpg.Services.CharacterService;
using dotnet_rpg.Dtos.Character;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace dotnet_rpg.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class CharacterController : ControllerBase
    {
        // private static List<Character> characters = new List<Character>(){
        //     new Character(),
        //     new Character{
        //         Id = 1,
        //         Name = "Sam"
        //     }
        // };
        private readonly ICharacterServices  _characterServices;

        public CharacterController(ICharacterServices characterServices){
            _characterServices = characterServices;
        }

       // [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get(){
            int userId = int.Parse(User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _characterServices.GetAllCharacters(userId));
           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id){
            //return Ok(characters.FirstOrDefault(c=>c.Id == id));
             return Ok(await _characterServices.GetAllCharacterById(id));
        }

      //  [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter){
            // characters.Add(newCharacter);
            // return Ok(characters);
            return Ok(await _characterServices.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacter(UpdateCharacterDto updateCharacter){

           
           var response = await _characterServices.UpdateCharacter(updateCharacter);
           if(response.Data == null){
               return NotFound(response);
           }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id){
            var response = await _characterServices.DeleteCharacter(id);
           if(response.Data == null){
               return NotFound(response);
           }
            return Ok(response);
        }

    }
}

