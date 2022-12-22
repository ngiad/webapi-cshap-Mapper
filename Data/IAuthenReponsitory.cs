using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cshap_basic_vscode.Data
{
    public interface IAuthenReponsitory
    {
        Task<ServiceResponse<int>> Register(User newUser,string password);
        Task<ServiceResponse<string>> Login(string username,string Password);
        Task<bool>UserExists(string username);
    }
}