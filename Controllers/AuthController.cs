using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cshap_basic_vscode.Dtos.User;

namespace cshap_basic_vscode.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenReponsitory _authenRepo;
        public AuthController(IAuthenReponsitory authenRepo){
            _authenRepo = authenRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request){

            var res = await _authenRepo.Register(
                new User {Username = request.Username},request.Password
            );
            
            if(!res.success)
                return BadRequest(res);
            
            return Ok(res);
        }

        [HttpPost("Login")] 

        public async Task<ActionResult<ServiceResponse<string>>> Login (UserLoginDto userLogin){
            var res = await _authenRepo.Login(
                userLogin.Username ,userLogin.Password
            );

            if(!res.success)
                return BadRequest(res);

            return Ok(res);
        }
    }
}