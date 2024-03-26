using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using YoloHomeAPI;
using YoloHomeAPI.Contexts;
using YoloHomeAPI.Data;
using YoloHomeAPI.Interfaces;

namespace YoloHomeAPI.Services
{

    public class AuthenticationService : IAuthenticationService
    {
        private readonly Settings _settings;
        //private readonly YoloHomeDbContext _context;

        //public AuthenticationService(YoloHomeDbContext context, Settings settings)
        public AuthenticationService(Settings settings)
        {
            //_context = context;
            _settings = settings;
        }

        public IAuthenticationService.AuthenticationResult Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return new(false, "Username or password cannot be empty.", "");
            }

            /*
            if (_context.Users.Any(x => x.UserName == username))
            {
                return new(false, "Username already exists.", "");
            }
            */

            var (hashedPassword, salt) = HashPassword(password);

            var user = new User
            {
                UserName = username,
                PasswordHash = hashedPassword,
                PasswordSalt = salt
            };

            //_context.Users.Add(user);
            //_context.SaveChanges();

            return new(true, "User created successfully.", GenerateJwtToken(user.UserId.ToString()));
        }


        public IAuthenticationService.AuthenticationResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return new(false, "Username or password cannot be empty.", "");
            }

            /*
            var user = _context.Users.SingleOrDefault(x => x.UserName == username);

            if (user == null)
            {
                return new(false, "Username or password is incorrect.", "");
            }

            if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                return new(false, "Username or password is incorrect.", "");
            }
            return new(true, "Login successful.", GenerateJwtToken(user.UserId.ToString()));
            
            */
            return new(true, "Login successful.", GenerateJwtToken("123456789"));
        }

        private static (string hash, string salt) HashPassword(string password)
        {
            using var hmac = new HMACSHA512();
            var salt = Convert.ToBase64String(hmac.Key);
            var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return (hash, salt);
        }

        private static bool VerifyPassword(string password, string hash, string salt)
        {
            using var hmac = new HMACSHA512(Convert.FromBase64String(salt));
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return computedHash == hash;
        }

        private string GenerateJwtToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.JwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", userId) }),
                Expires = DateTime.UtcNow.AddDays(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }

}