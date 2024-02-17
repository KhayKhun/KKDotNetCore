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

        [HttpGet]
        public IActionResult GetUsers()
        {
            List<UserDataModel> lst = _dbContext.User.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            UserDataModel? item = _dbContext.User.FirstOrDefault(x => x.UserId == id);
            if (item is null)
            {
                return NotFound("No user found");
            }
            return Ok(item);
        }

        [HttpGet("pegi")]
        public IActionResult GetPeginationBlogs(int PageNo, int PageSize)
        {
            var lst = _dbContext.User
                .Skip((PageNo - 1) * PageSize)
                .Take(PageSize)
                .ToList();
            int RowCount = _dbContext.Users.Count();
            int PageCount = RowCount / PageSize;
            if (RowCount % PageSize > 0)
            {
                PageCount++;
            }

            return Ok(new
            {
                IsEndOfPage = PageNo >= PageCount,
                pageCount = PageCount,
                pageSize = PageSize,
                pageNo = PageNo,
                data = lst,
            });

        }

        [HttpPost]
        public IActionResult CreateUser(UserDataModel user)
        {
            _dbContext.User.Add(user);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Saved user." : "Failed to save";

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
                return NotFound("No user found");
            }

            item.UserName = user.UserName;
            item.UserPhone = user.UserPhone;
            item.UserAddress = user.UserAddress;
            item.UserEmail = user.UserEmail;

            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Updated user." : "Failed to update";

            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchUser(int id, UserDataModel user)
        {
            UserDataModel? item = _dbContext.User.FirstOrDefault(x => x.UserId == id);

            if (item is null)
            {
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

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsers(int id)
        {
            UserDataModel? item = _dbContext.User.FirstOrDefault(x => x.UserId == id);

            if (item is null)
            {
                return NotFound("User not found");
            }
            _dbContext.Remove(item);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Deleted user" : "Failed to delete";

            return Ok(message);
        }
    }
}
