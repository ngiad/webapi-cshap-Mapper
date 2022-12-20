using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace cshap_basic_vscode.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterApiController : ControllerBase
    {
        private readonly ICharecterService _charecterService;

        public CharacterApiController(ICharecterService charecterService){
            _charecterService = charecterService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            return Ok(await _charecterService.GetAllCharecters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await _charecterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _charecterService.AddCharacter(newCharacter));
        }

        [HttpPut]

        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateChacterDto updateCharecter){
            ServiceResponse<GetCharacterDto> res = await _charecterService.UpdateCharacter(updateCharecter);
            if(res.data is null){
                return NotFound(res);
            }
            return Ok(res);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id){
            ServiceResponse<List<GetCharacterDto>> res = await _charecterService.DeleteCharacter(id);
            if(res.data is null){
                return NotFound(res);
            }
            return Ok(res);
        }

    }
}