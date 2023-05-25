using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using libre_pansador_api.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using libre_pansador_api.Interfaces;

namespace libre_pansador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _employees;

        public AuthenticationController(IConfiguration configuration, IUserService employees)
        {
            this._configuration = configuration;
            this._employees = employees;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(401)]
        public IActionResult Authenticate([FromBody] UserCredentials credentials)
        {
            Models.User? employee = this.GetUserWhitCredentials(credentials);
            if (employee == null)
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
                    new Claim("IsAdmin", employee.IsAdmin.ToString())
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
                Domain = "localhost",
                Expires = DateTime.Today.AddDays(1)
            };
            Response.Cookies.Append("access_token", tokenString, cookieOptions);

            return Ok(new { status = "Authenticated" });
        }

        
        [HttpGet("checkIfAuthenticated")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(401)]
        public IActionResult CheckIfAuthenticated()
        {
            return Ok(new { status = "Authenticated" });
        }


        private Models.User? GetUserWhitCredentials(UserCredentials credentials)
        {
            var employee = this._employees.Read(credentials.Username);
            if (employee == null)
                return null;

            bool areCredentialsValid = employee.UserName == credentials.Username && employee.Password == credentials.Password;
            return areCredentialsValid ? employee : null;
        }
    }
}
