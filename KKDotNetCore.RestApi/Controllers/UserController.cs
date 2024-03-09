using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KKDotNetCore.RestApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbContext = new AppDbContext();
        private readonly ILog _logger;

        public UserController(ILog logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpGet]
        public IActionResult GetUsers()
        {
            List<UserDataModel> lst = _dbContext.User.ToList();
            _logger.Info("GetUsers: response user lst.");
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            UserDataModel? item = _dbContext.User.FirstOrDefault(x => x.UserId == id);
            if (item is null)
            {
                _logger.Debug($"GetUser: user with userId={id} is null.");
                return NotFound("No user found");
            }
            _logger.Info("GetUser: response user.");
            return Ok(item);
        }

        [HttpGet("pegi")]
        public IActionResult GetPeginationBlogs(int PageNo, int PageSize)
        {
            var lst = _dbContext.User
                .Skip((PageNo - 1) * PageSize)
                .Take(PageSize)
                .ToList();
            int RowCount = _dbContext.User.Count();
            int PageCount = RowCount / PageSize;
            if (RowCount % PageSize > 0)
            {
                PageCount++;
            }

            _logger.Debug($"GetPeginationBlogs: response users pageNo={PageNo}, pageSize={PageSize}, RowCount={RowCount}, PageCount={PageCount}.");

            var data = new
            {
                rowCount = RowCount,
                IsEndOfPage = PageNo >= PageCount,
                pageCount = PageCount,
                pageSize = PageSize,
                pageNo = PageNo,
                data = lst,
            };

            return Ok(data);

        }

        [HttpPost]
        public IActionResult CreateUser(UserDataModel user)
        {
            _dbContext.User.Add(user);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Saved user." : "Failed to save";

            _logger.Debug($"CreateUser: {message}");
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserDataModel user)
        {
            Console.WriteLine(id);
            UserDataModel? item = _dbContext.User.FirstOrDefault(x => x.UserId == id);
            Console.WriteLine($"user => {item is null}");
            if (item is null)
            {
                _logger.Debug($"UpdateUser: user with userId={id} is null");
                return NotFound("No user found");
            }

            item.UserName = user.UserName;
            item.UserPhone = user.UserPhone;
            item.UserAddress = user.UserAddress;
            item.UserEmail = user.UserEmail;

            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Updated user." : "Failed to update";

            _logger.Debug($"UpdateUser: {message}");
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchUser(int id, UserDataModel user)
        {
            UserDataModel? item = _dbContext.User.FirstOrDefault(x => x.UserId == id);

            if (item is null)
            {
                _logger.Debug($"PatchUser: user with userId={id} is null");
                return NotFound("No user found");
            }
            if (!string.IsNullOrEmpty(user.UserPhone))
            {
                item.UserPhone = user.UserPhone;
            }
            if (!string.IsNullOrEmpty(user.UserName))
            {
                item.UserName = user.UserName;
            }
            if (!string.IsNullOrEmpty(user.UserAddress))
            {
                item.UserAddress = user.UserAddress;
            }
            if (!string.IsNullOrEmpty(user.UserEmail))
            {
                item.UserEmail = user.UserEmail;
            }

            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Updated user." : "Failed to update";

            _logger.Debug($"PatchUser: {message}");
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsers(int id)
        {
            UserDataModel? item = _dbContext.User.FirstOrDefault(x => x.UserId == id);

            if (item is null)
            {
                _logger.Debug($"DeleteUser: user with userId={id} is null");
                return NotFound("User not found");
            }
            _dbContext.Remove(item);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Deleted user" : "Failed to delete";

            _logger.Debug($"DeleteUser: {message}");
            return Ok(message);
        }
    }
}
