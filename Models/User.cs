using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cshap_basic_vscode.Models
{
    public class User
    {
        public int Id {get; set;}
        public string Username {get;set;} = "Unknown";
        public byte[] PasswordHash {get;set;} = new byte[0];
        public byte[] PasswordSalt {get;set;} = new byte[0];
        public List<Character> Characters {get;set;}

    }
}