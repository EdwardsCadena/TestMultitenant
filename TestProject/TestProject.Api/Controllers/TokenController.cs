﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestProject.Core.Entities;
using TestProject.Core.Interfaces;
using TestProject.Infrastructure.Interfaces;

namespace TestProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public TokenController(IConfiguration configuration, IUserRepository userRepository, IPasswordService passwordService)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        // POST api/<TokenController>
        [HttpPost]
        public async Task<IActionResult> Authentication(UserLogin login)
        {
            //if it is a valid user
            var validation = await IsValidUser(login);
            if (validation.Item1)
            {
                var token = GenerateToken(validation.Item2);
                return Ok(new { token });
            }

            return NotFound();
        }

        private async Task<(bool, User)> IsValidUser(UserLogin login)
        {
            var user = await _userRepository.GetLoginByCredentials(login);
            var isValid = _passwordService.Check(user.Password, login.Password);
            return (isValid, user);
        }

        private string GenerateToken(User user)
        {

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);


            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("User", user.Email),
            };


            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(10)
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }

}