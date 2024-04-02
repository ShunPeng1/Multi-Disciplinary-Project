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
using YoloHomeAPI.Services.Interfaces;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Services
{

    public class MockAuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationSettings _authenticationSettings;
        //private readonly YoloHomeDbContext _context;

        //public AuthenticationService(YoloHomeDbContext context, AuthenticationSettings authenticationSettings)
        public MockAuthenticationService(AuthenticationSettings authenticationSettings)
        {
            //_context = context;
            _authenticationSettings = authenticationSettings;
        }

        public IAuthenticationService.AuthenticationResult Register(string username, string password)
        {

            return new(true, "User created successfully.", GenerateJwtToken("123456789"));
        }


        public IAuthenticationService.AuthenticationResult Login(string username, string password)
        {
            
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
            var key = Encoding.ASCII.GetBytes(_authenticationSettings.JwtKey);

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