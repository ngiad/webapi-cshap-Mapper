using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cshap_basic_vscode.Models
{
    public class Character
    {
        public int Id {get;set;}
        public string Name {get;set;} = "Unknown";
        public int HitPoints {get;set;} = 100;
        public int Strength {get;set;} = 10;
        public int Defense {get;set;} = 10;
        public int Interlligence {get;set;} = 10;
        public RpgClass Class {get;set;} = RpgClass.Knight;
    }
}