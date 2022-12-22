using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cshap_basic_vscode.Data
{
    public class AuthenReponsitory : IAuthenReponsitory
    {
        private readonly DataContext _context;
        public AuthenReponsitory(DataContext context){
            _context = context;
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
                res.data = user.Id.ToString();
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
    }
}