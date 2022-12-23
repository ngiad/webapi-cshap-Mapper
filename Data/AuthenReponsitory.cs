using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace cshap_basic_vscode.Data
{
    public class AuthenReponsitory : IAuthenReponsitory
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AuthenReponsitory(DataContext context,IConfiguration configuration){
            _context = context;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<string>> Login(string username, string Password)
        {
            var res = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));

            if(user is null){
                res.success = false;
                res.message = "User Not Found!";
            }

            else if(!VerifyPassword(Password,user.PasswordHash,user.PasswordSalt)){
                res.success = false;
                res.message = "Wrong password!";
            }else{
                res.data = CreateToken(user);
            }

            return res;
        }

        public async Task<ServiceResponse<int>> Register(User newUser, string password)
        {
            ServiceResponse<int> res = new ServiceResponse<int>();

            if(await UserExists(newUser.Username)){
                res.success = false;
                res.message = "User already exists!";
                return res;
            }
            CreatePasswordHash(password,out byte[] passwordHash,out byte[] passwordSalt);

            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();


            res.data = newUser.Id;
            return res;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower())){
                return true;
            }

            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash,out byte[] passwordSalt){
            using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[]passwordHash ,byte[] passwordSalt){
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)){
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }


        private string CreateToken(User user){
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username)
            };

            var appSettingsToken = _configuration.GetSection("AppSetting:Token").Value;

            if(appSettingsToken is null)
                throw new Exception("AppSettings Token is null!");
            
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

            SigningCredentials creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1), //time
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}