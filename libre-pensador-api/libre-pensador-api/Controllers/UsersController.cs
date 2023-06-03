using libre_pensador_api.Interfaces;
using libre_pensador_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Org.BouncyCastle.Asn1.Ocsp;

namespace libre_pensador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _users;

        public UsersController(IUserService usersService)
        {
            this._users = usersService;
        }

        // GET: api/<UsersController>/userName
        [HttpGet("{userName}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult Get(string userName)
        {
            User? user = this._users.Read(userName);
            if(user == null)
                return NotFound();
            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult Post([FromBody] User user)
        {
            user.IsAdmin = false;
            User? createdUser = this._users.Create(user);
            if (createdUser == null)
                return StatusCode(500, "Unexpected error while creating the user, the user wasn't created");
            return Ok(createdUser);
        }

        // DELETE api/<UsersController>/userName
        [HttpDelete("{userName}")]
        public IActionResult Delete(string userName)
        {
            User? deletedUser = this._users.Delete(userName);
            if(deletedUser == null)
                return NotFound();
            return Ok(deletedUser);
        }
    }
}
