﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using libre_pensador_api.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using libre_pensador_api.Interfaces;

namespace libre_pensador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _users;

        public AuthenticationController(IConfiguration configuration, IUserService users)
        {
            this._configuration = configuration;
            this._users = users;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(401)]
        public IActionResult Authenticate([FromBody] UserCredentials credentials)
        {
            Models.User? user = this._users.ReadUserWhitCredentials(credentials);
            if (user == null)
                return Unauthorized();

            string? secretKeyConfig = this._configuration["Jwt:SecretKey"];
            if (string.IsNullOrEmpty(secretKeyConfig))
                throw new Exception("The secret key is missing or invalid.");

            var secretKey = Encoding.UTF8.GetBytes(secretKeyConfig);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, credentials.Username),
                    new Claim("IsAdmin", user.IsAdmin.ToString())
                }),
                NotBefore = DateTime.Today,
                Expires = DateTime.Today.AddDays(1),
                Audience = this._configuration["Jwt:Audience"],
                Issuer = this._configuration["Jwt:Issuer"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Today.AddDays(7)
            };
            Response.Cookies.Append("access_token", tokenString, cookieOptions);

            return Ok(new { IsAdmin = user.IsAdmin });
        }

        
        [HttpGet("checkIfAuthenticated")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(401)]
        public IActionResult CheckIfAuthenticated()
        {
            return Ok(new { status = "Authenticated" });
        }
    }
}
