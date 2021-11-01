using Common;
using Common.DTO;
using Common.RepositoryInterfaces;
using Common.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ITokenGenerationService
    {
        public Task<string> ValidateUserAndGetJWT(UserLoginViewModel loginModel);
    }

    public class TokenGenerationService : ITokenGenerationService
    {
        private readonly IUserAccessRepository _userRepository;
        private readonly IConfiguration _configuration;

        public TokenGenerationService(IUserAccessRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> ValidateUserAndGetJWT(UserLoginViewModel loginModel)
        {
            var securityHelper = new SecurityHelper();
            string HashedPassword = securityHelper.Getsha256Hash(loginModel.Password);
            var Person = await _userRepository.ValidateUser(new UserLoginDTO() { UserName = loginModel.UserName, HashedPassword = HashedPassword });
            if (Person)
            {
                var token = GenerateToken(loginModel);
                return token;
            }
            return null;
        }


        public string GenerateToken(UserLoginViewModel loginModel)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.UserName ?? throw new InvalidOperationException()),
            };

            string key = _configuration["JWtConfig:Key"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWtConfig:issuer"],
                audience: _configuration["JWtConfig:audience"],
                claims: claims,
                signingCredentials: credentials
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }

    }
}
