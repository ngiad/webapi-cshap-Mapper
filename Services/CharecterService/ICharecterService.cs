using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cshap_basic_vscode.Services.CharecterService
{
    public interface ICharecterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>>GetAllCharecters();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharecter);
        Task<ServiceResponse<GetCharacterDto>>UpdateCharacter(UpdateChacterDto updateCharecter);
        Task<ServiceResponse<List<GetCharacterDto>>>DeleteCharacter(int id);
    }
}