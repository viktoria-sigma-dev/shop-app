using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersApp.Dto;
using UsersApp.Model;
using UsersApp.Services;

namespace UsersApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<UsersController> _logger;
        public UsersController(UserService userService, ILogger<UsersController> logger)
        {
            this._userService = userService;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> GetAll([FromQuery] bool extended = false)
        {
            _logger.LogInformation("GetAll endpoint hit with extended={Extended}", extended);
                if (extended)
                    return Ok(this._userService.GetAll());
                return Ok(this._userService.GetAllDTOs());
        }

        [HttpGet("{userId}")]
        public ActionResult<object> GetOne([FromRoute] int userId, [FromQuery] bool extended = false)
        {
                if (extended)
                {
                    var dto = _userService.GetById(userId);
                    if (dto == null)
                        return NotFound($"User with ID {userId} not found.");
                    return Ok(dto);
                }


                var user = this._userService.GetDTOById(userId);
                if (user == null)
                    return NotFound($"User with ID {userId} not found.");
                return Ok(user);
        }

        [HttpPost("create")]
        public ActionResult<User> Create([FromBody] CreateUserDTO dto)
        {
            try
            {
                var created = _userService.Create(dto);
                return CreatedAtAction(nameof(GetOne), new { userId = created.Id }, created);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{userId}")]
        public ActionResult<User> Update([FromRoute] int userId, [FromBody] UpdateUserDTO dto)
        {
            try
            {
                var updated = _userService.Update(userId, dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{userId}")]
        public ActionResult<User> Delete([FromRoute] int userId)
        {
            try
            {
                var deleted = _userService.Delete(userId);
                
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}