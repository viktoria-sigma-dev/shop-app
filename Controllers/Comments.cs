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
    public class CommentsController : ControllerBase
    {
        private readonly CommentService _commentService;
        public CommentsController(CommentService commentService)
        {
            this._commentService = commentService;
        }

        [HttpGet("{userId}")]
        public ActionResult<List<Comment>> GetAll([FromRoute] int userId)
        {
            try
            {
                return Ok(this._commentService.GetAll(userId));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("{userId}")]
        public ActionResult<Comment> Create([FromRoute] int userId, [FromBody] CreateCommentDTO dto)
        {
            try
            {
                var created = _commentService.Create(userId, dto);
                if (created == null)
                    return NotFound($"User with ID {userId} not found.");

                return CreatedAtAction(nameof(GetAll), new { userId = userId }, created);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{userId}/{commentId}")]
        public ActionResult<Comment> Delete([FromRoute] int userId, [FromRoute] int commentId)
        {
            try
            {
                var deleted = _commentService.Delete(userId, commentId);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}