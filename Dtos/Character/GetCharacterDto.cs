using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cshap_basic_vscode.Dtos.Character
{
    public class GetCharacterDto
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public int HitPoints {get;set;} 
        public int Strength {get;set;} 
        public int Defense {get;set;} 
        public int Interlligence {get;set;}
        public RpgClass Class {get;set;} 
    }
}