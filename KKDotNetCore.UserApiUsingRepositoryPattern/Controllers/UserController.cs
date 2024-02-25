using Contracts;
using KKDotNetCore.UserApiUsingRepositoryPattern.Entities.Models;
using KKDotNetCore.UserApiUsingRepositoryPattern.Repositories.UserRepository;
using Microsoft.AspNetCore.Mvc;

namespace KKDotNetCore.UserApiUsingRepositoryPattern
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoggerManager _logger;

        public UserController( IUserRepository userRepository, ILoggerManager logger )
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var lst = await _userRepository.GetUsers();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetUser(id);
                if(user is null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                var result = await _userRepository.CreateUser(user);
                string message = result > 0 ? "Saved user." : "Failed to save";
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            try
            {
                var result = await _userRepository.UpdateUser(id,user);
                string message = result > 0 ? "Updated user." : "Failed to update";
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _userRepository.DeleteUser(id);
                string message = result > 0 ? "Deleted user." : "Failed to delete";
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
    }
}
