using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using libre_pansador_api.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;

namespace libre_pansador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(401)]
        public IActionResult Authenticate([FromBody] UserCredentials credentials)
        {
            if (!IsValidUser(credentials))
                return Unauthorized();

            string? secretKeyConfig = this._configuration["Jwt:SecretKey"];
            if (string.IsNullOrEmpty(secretKeyConfig))
                throw new Exception("The secret key is missing or invalid.");

            var secretKey = Encoding.UTF8.GetBytes(secretKeyConfig);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, credentials.Username) }),
                NotBefore = DateTime.Today,
                Expires = DateTime.Today.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        private bool IsValidUser(UserCredentials credentials)
        { 
            return credentials.Username == "LibrePensadorRaiz" && credentials.Password == "Adminlibrepensador";
        }
    }
}
