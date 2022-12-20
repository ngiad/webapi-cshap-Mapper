using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cshap_basic_vscode
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile(){
            CreateMap<Character,GetCharacterDto>();
            CreateMap<AddCharacterDto,Character>();
            CreateMap<UpdateChacterDto,Character>();
        }
    }
}