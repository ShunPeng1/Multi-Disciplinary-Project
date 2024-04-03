﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using YoloHomeAPI;
using YoloHomeAPI.Contexts;
using YoloHomeAPI.Data;
using YoloHomeAPI.Services.Interfaces;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Services
{

    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationSettings _authenticationSettings;
        //private readonly YoloHomeDbContext _context;

        //public AuthenticationService(YoloHomeDbContext context, AuthenticationSettings authenticationSettings)
        public AuthenticationService(AuthenticationSettings authenticationSettings)
        {
            //_context = context;
            _authenticationSettings = authenticationSettings;
        }

        
        public IAuthenticationService.AuthenticationResult Register(string username, string password)
        {
            if (CheckNull(username) || CheckNull(password))
            {
                return new(false, "Username or password cannot be empty.", "");
            }
            
            if (CheckIllegalCharacterInUserName(username))
            {
                return new(false, "Username contains illegal characters.", "");
            }
            
            if (CheckCharacterInPassword(password))
            {
                return new(false, "Password must contain at least one uppercase letter, one lowercase letter, one number, one special character and be at least 8 characters long.", "");
            }
            
            if (CheckLength(username, 3, 20))
            {
                return new(false, "Username must be between 3 and 20 characters long.", "");
            }
            
            if (CheckLength(password, 8, 20))
            {
                return new(false, "Password must be between 8 and 20 characters long.", "");
            }
            

            /*
            if (_context.Users.Any(x => x.UserName == userName))
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
            if (CheckNull(username) || CheckNull(password))
            {
                return new(false, "Username or password cannot be empty.", "");
            }
            
            if (CheckIllegalCharacterInUserName(username))
            {
                return new(false, "Username contains illegal characters.", "");
            }
            
            if (CheckCharacterInPassword(password))
            {
                return new(false, "Password must contain at least one uppercase letter, one lowercase letter, one number, one special character and be at least 8 characters long.", "");
            }
            
            if (CheckLength(username, 3, 20))
            {
                return new(false, "Username must be between 3 and 20 characters long.", "");
            }
            
            if (CheckLength(password, 8, 20))
            {
                return new(false, "Password must be between 8 and 20 characters long.", "");
            }

            /*
            var user = _context.Users.SingleOrDefault(x => x.UserName == userName);

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
        
        private bool CheckNull(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return true;
            }
            return false;
        }

        private bool CheckIllegalCharacterInUserName(string userName)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            if (rgx.IsMatch(userName))
            {
                return true;
            }
            return false;
            
        }
        
        private bool CheckCharacterInPassword(string password)
        {
            Regex rgx = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$");
            if (rgx.IsMatch(password))
            {
                return true;
            }
            return false;
        }
        
        private bool CheckLength(string input, int min, int max)
        {
            if (input.Length < min || input.Length > max)
            {
                return true;
            }
            return false;
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