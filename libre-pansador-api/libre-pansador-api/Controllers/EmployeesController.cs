using libre_pansador_api.Interfaces;
using libre_pansador_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Org.BouncyCastle.Asn1.Ocsp;

namespace libre_pansador_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeess;

        public EmployeesController(IEmployeesService employeesService)
        {
            this._employeess = employeesService;
        }

        // GET: api/<EmployeesController>/userName
        [HttpGet("{userName}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult Get(string userName)
        {
            Employee? employee = this._employeess.Read(userName);
            if(employee == null)
                return NotFound();
            return Ok(employee);
        }

        // POST api/<EmployeesController>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Employee))]
        public IActionResult Post([FromBody] Employee employee)
        {
            Employee? createdEmployee = this._employeess.Create(employee);
            if (createdEmployee == null)
                return StatusCode(500, "Unexpected error while creating the employee, the employee wasn't created");
            return Ok(createdEmployee);
        }

        // PUT api/<EmployeesController>/userName
        [HttpPut("{userName}")]
        public IActionResult Put(string userName, [FromBody] Models.RequestModels.UpdateEmployeeRequest updateEmployee)
        {
            Employee? updatedEmployee = this._employeess.Update(userName, updateEmployee);
            if (updatedEmployee == null)
                return NotFound();
            return Ok(updatedEmployee);
        }

        // DELETE api/<EmployeesController>/userName
        [HttpDelete("{userName}")]
        public IActionResult Delete(string userName)
        {
            Employee? deletedEmployee = this._employeess.Delete(userName);
            if(deletedEmployee == null)
                return NotFound();
            return Ok(deletedEmployee);
        }
    }
}
