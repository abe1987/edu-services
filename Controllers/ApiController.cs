using edu_services.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;

namespace edu_services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly Classroom _classroom;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
            _classroom = new Classroom();
        }

        //TODO: Implement routes & domain using the Classroom object.

        [HttpPost("teacher")]
        public async Task<IActionResult> AddTeacherAsync([FromBody] Teacher teacher)
        {
            try
            {
                await _classroom.AddTeacherAsync(teacher);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("students")]
        public async Task<IActionResult> AddStudentsAsync([FromBody] List<Student> students)
        {
            try
            {
                await _classroom.AddStudentsAsync(students);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("roster")]
        public async Task<IActionResult> GetRosterAsync()
        {
            try
            {
                var roster = await _classroom.GetRosterAsync();
                return Ok(roster);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
