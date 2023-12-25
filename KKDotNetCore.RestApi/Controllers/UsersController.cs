using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _dbContext = new AppDbContext();

        [HttpGet]
        public IActionResult GetUsers()
        {
            List<UserDataModel> lst = _dbContext.Users.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            UserDataModel? item = _dbContext.Users.FirstOrDefault(x => x.User_Id == id);
            if(item is null)
            {
                return NotFound("No user found");
            }
            return Ok(item);
        }

        [HttpGet("pegi")]
        public IActionResult GetPeginationBlogs(int PageNo, int PageSize)
        {
            var lst = _dbContext.Users
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
            _dbContext.Users.Add(user);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Saved user." : "Failed to save";

            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id,UserDataModel user)
        {
            UserDataModel? item = _dbContext.Users.FirstOrDefault(x => x.User_Id == id);

            if (item is null)
            {
                return NotFound("No user found");
            }

            item.User_Name = user.User_Name;
            item.User_Age = user.User_Age;

            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Updated user." : "Failed to update";

            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchUser(int id, UserDataModel user)
        {
            UserDataModel? item = _dbContext.Users.FirstOrDefault(x => x.User_Id == id);

            if (item is null)
            {
                return NotFound("No user found");
            }
            if(!string.IsNullOrEmpty(user.User_Name))
            {
                item.User_Name = user.User_Name;
            }
            if(user.User_Age > 0)
            {
                item.User_Age = user.User_Age;
            }

            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Updated user." : "Failed to update";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsers(int id)
        {
            UserDataModel? item = _dbContext.Users.FirstOrDefault(x => x.User_Id == id);

            if(item is null)
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
