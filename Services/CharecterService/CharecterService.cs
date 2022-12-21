using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace cshap_basic_vscode.Services.CharecterService
{
    public class CharecterService : ICharecterService
    {
        // private static List<Character> Characters = new List<Character>(){
        //     new Character(),
        //     new Character{Name = "Nghia",Id = 1}
        // };

        public readonly IMapper _mapper;
        public readonly DataContext _context;

        public CharecterService(IMapper mapper,DataContext context){
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharecter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharecter);
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            serviceResponse.data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharecters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {   
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter =  await _context.Characters.FirstOrDefaultAsync(c=> c.Id == id);
            if(dbCharacter is not null){
                serviceResponse.data = _mapper.Map<GetCharacterDto>(dbCharacter);
                return serviceResponse;
            } 
            throw new Exception("Character not Found");
        }

        public async Task<ServiceResponse<GetCharacterDto>>UpdateCharacter(UpdateChacterDto updateCharecter){
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updateCharecter.Id);

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

                await _context.SaveChangesAsync();

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
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

                if(character is null){
                    throw new Exception($"Character id {id} not Found!");
                }

                _context.Characters.Remove(character);

                await _context.SaveChangesAsync();
                serviceResponse.data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            }
            catch (System.Exception error)
            {
                serviceResponse.success = false;
                serviceResponse.message = error.Message;
            }
           

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> QueryCharacter(string name){
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                List<Character> Characters = await _context.Characters.Where(c => c.Name.Contains(name)).ToListAsync();
                if(Characters.Count == 0){
                    throw new Exception($"Character name {name} notFound!");
                }

                serviceResponse.data = Characters.Select(c =>  _mapper.Map<GetCharacterDto>(c)).ToList();
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