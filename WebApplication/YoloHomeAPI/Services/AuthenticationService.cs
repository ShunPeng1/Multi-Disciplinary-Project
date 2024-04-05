using System;
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
using YoloHomeAPI.Repositories;
using YoloHomeAPI.Repositories.Interfaces;

namespace YoloHomeAPI.Services
{

    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationSettings _authenticationSettings;

        private readonly IUserRepo _userRepo;
        

        //public AuthenticationService(YoloHomeDbContext context, AuthenticationSettings authenticationSettings)
        public AuthenticationService(AuthenticationSettings authenticationSettings, IUserRepo userRepo)
        {
            _authenticationSettings = authenticationSettings;
            _userRepo = userRepo;
        }

        
        public async Task<IAuthenticationService.AuthenticationResult> Register(string username, string password)
        {
            if (CheckNull(username) || CheckNull(password))
            {
                return new(false, "Username or password cannot be empty.", "");
            }
            
            if (!CheckLegalCharacterInUserName(username))
            {
                return new(false, "Username contains illegal characters.", "");
            }
            
            if (!CheckLegalCharacterInPassword(password))
            {
                return new(false, "Password must contain at least one uppercase letter, one lowercase letter, one number, one special character and be at least 8 characters long.", "");
            }
            
            if (!CheckLength(username, 3, 20))
            {
                return new(false, "Username must be between 3 and 20 characters long.", "");
            }
            
            if (!CheckLength(password, 8, 20))
            {
                return new(false, "Password must be between 8 and 20 characters long.", "");
            }

            // Check if username already exists in database
            User? user = await _userRepo.GetByUserAsync(username);
            if (user != null)
            {
                return new(false, "Username already exists.", "");
            }

            var (hashedPassword, salt) = HashPassword(password);

            var newUser = new User
            {
                UserName = username,
                PasswordHash = hashedPassword,
                PasswordSalt = salt
            };
            
            // Save user to database
            await _userRepo.AddAsync(newUser);
            
            
            return new(true, "User created successfully.", GenerateJwtToken(newUser.UserName));
        }


        public async Task<IAuthenticationService.AuthenticationResult> Login(string username, string password)
        {
            if (CheckNull(username) || CheckNull(password))
            {
                return new(false, "Username or password cannot be empty.", "");
            }
            
            if (!CheckLegalCharacterInUserName(username))
            {
                return new(false, "Username contains illegal characters.", "");
            }
            
            if (!CheckLegalCharacterInPassword(password))
            {
                return new(false, "Password must contain at least one uppercase letter, one lowercase letter, one number, one special character and be at least 8 characters long.", "");
            }
            
            if (!CheckLength(username, 3, 20))
            {
                return new(false, "Username must be between 3 and 20 characters long.", "");
            }
            
            if (!CheckLength(password, 8, 20))
            {
                return new(false, "Password must be between 8 and 20 characters long.", "");
            }

            // Check if username already exists in database
            User? user = await _userRepo.GetByUserAsync(username);
            
            if (user == null)
            {
                return new(false, "Username or password is incorrect.", "");
            }

            if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                return new(false, "Username or password is incorrect.", "");
            }
            return new(true, "Login successful.", GenerateJwtToken(user.UserName));
            
            
        }
        
        private bool CheckNull(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        private bool CheckLegalCharacterInUserName(string userName)
        {
            Regex rgx = new Regex("[a-zA-Z0-9 -]");
            return rgx.IsMatch(userName);
        }
        
        private bool CheckLegalCharacterInPassword(string password)
        {
            Regex rgx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return rgx.IsMatch(password);
        }
        
        private bool CheckLength(string input, int min, int max)
        {
            return input.Length >= min || input.Length <= max;
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