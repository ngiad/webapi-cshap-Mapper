using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace cshap_basic_vscode.Services.CharecterService
{
    public class CharecterService : ICharecterService
    {
        private static List<Character> Characters = new List<Character>(){
            new Character(),
            new Character{Name = "Nghia",Id = 1}
        };

        public readonly IMapper _mapper;

        public CharecterService(IMapper mapper){
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharecter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharecter);
            character.Id = Characters.Max(c => c.Id) + 1;
            Characters.Add(character);
            serviceResponse.data = Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharecters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.data = Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {   
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character = Characters.Find(c => c.Id == id); 
            if(character is not null){
                serviceResponse.data = _mapper.Map<GetCharacterDto>(character);
                return serviceResponse;
            } 
            throw new Exception("Character not Found");
        }

        public async Task<ServiceResponse<GetCharacterDto>>UpdateCharacter(UpdateChacterDto updateCharecter){
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                Character character = Characters.Find(c => c.Id == updateCharecter.Id);

                if(character is null){
                    throw new Exception($"Character id {updateCharecter.Id} not Found!");
                }

                _mapper.Map(updateCharecter,character);

                character.Name = updateCharecter.Name;
                character.HitPoints = updateCharecter.HitPoints;
                character.Strength = updateCharecter.Strength;
                character.Defense = updateCharecter.Defense;
                character.Interlligence = updateCharecter.Interlligence;
                character.Class = updateCharecter.Class;

                serviceResponse.data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (System.Exception error)
            {
                serviceResponse.success = false;
                serviceResponse.message = error.Message;
            }
           

            return serviceResponse;
        }
        
        public async Task<ServiceResponse<List<GetCharacterDto>>>DeleteCharacter(int id){
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                Character character = Characters.Find(c => c.Id == id);

                if(character is null){
                    throw new Exception($"Character id {id} not Found!");
                }

                Characters.Remove(character);
                serviceResponse.data = Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (System.Exception error)
            {
                serviceResponse.success = false;
                serviceResponse.message = error.Message;
            }
           

            return serviceResponse;
        }
    }
}