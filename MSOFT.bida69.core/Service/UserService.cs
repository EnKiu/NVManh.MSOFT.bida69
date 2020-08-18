using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MSOFT.bida69.core.Helpers;
using MSOFT.BL;
using MSOFT.Entities;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MSOFT.BL.Interfaces;
using MSOFT.DL.Interfaces;

namespace MSOFT.bida69.Services
{
    public class UserService: EntityBL<User>, IUserBL
    {
        private readonly AppSettings _appSettings;
        IUserRepository _iUserRepository;
        public UserService(IUserRepository iUserRepository, IOptions<AppSettings> appSettings) :base(iUserRepository)
        {
            _iUserRepository = iUserRepository;
            _appSettings = appSettings.Value;
        }
        public object Register(User user)
        {
            user.PasswordHash = HashPassword(user.Password);
            return InsertEntity(user);
            //return HashPassword(user.Password);
        }

        /// <summary>
        /// Hash password
        /// </summary>
        /// <param name="password">Mật khẩu người dùng</param>
        /// <returns></returns>
        /// CreatedBy : NVMANH (16/01/2020)
        private string HashPassword(string password)
        {
            // generate a 128-bit salt using a secure PRNG
            //byte[] salt = new byte[128 / 8];

            byte[] salt = Encoding.Unicode.GetBytes("Salt");
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(salt);
            //}
            //string pworginal = Convert.ToBase64String(salt);

            //Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hash;
            //return new { Salt = salt, Password = password, PasswordHash = hash, Orginal = pworginal };
        }

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            //new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin", Role = Role.Admin },
            //new User { Id = 2, FirstName = "Normal", LastName = "User", Username = "user", Password = "user", Role = Role.User },
            //new User { Id = 3, FirstName = "Nguyễn", LastName = "Mạnh", Username = "mnv6789@gmail.com", Password = "12345678@Abc", Role = Role.Admin },
            //new User { Id = 4, FirstName = "Thân Văn", LastName = "Thịnh", Username = "bida69.com@gmail.com", Password = "12345678@Abc", Role = Role.Admin }
        };

       

        public User Authenticate(string username, string password)
        {
            // Lấy thông tin Users:
            var user = _iUserRepository.GetUserAuthenticate(username, HashPassword(password));
            //var user = _users.SingleOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleName)
                }),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<User> GetAll()
        {
            return _users.WithoutPasswords();
        }

        public User GetById(Guid id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            return user.WithoutPassword();
        }

        public object LogOut()
        {
            // authentication successful so generate jwt token
            var user = new User
            {
                Id = Guid.NewGuid(),
                RoleName = String.Empty,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleName)
                }),
                Expires = DateTime.UtcNow.AddSeconds(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return null;
        }
    }
}