using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KKDotNetCore.MvcApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AuthController(AppDbContext appDbContext) { _appDbContext = appDbContext; }

        public async Task<IActionResult>  Index()
        {
            var lst =await _appDbContext.Auth.ToListAsync();
            return View(lst);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(AuthUserModel reqModel)
        {
            Console.WriteLine(reqModel is null);

            if (reqModel is null) return BadRequest();

            var newUser = new AuthUserModel() {
                UserId = Guid.NewGuid(),
                UserName = reqModel.UserName,
                FullName = reqModel.FullName,
                Email = reqModel.Email,
                Password = reqModel.Password,
                Role = ""
            };

            await _appDbContext.Auth.AddAsync(newUser);
            int res = await _appDbContext.SaveChangesAsync();
            if (res == 0) return BadRequest();

            return Json(new { message = "Saving success"});
        }

        [HttpPost]
        public async Task<IActionResult>  Login(AuthUserModel reqModel)
        {
            var user = await _appDbContext.Auth.FirstOrDefaultAsync(x => x.Email == reqModel.Email );

            if (user is null) return NotFound("User not found");

            if (user.Password == reqModel.Password) {
                return Json(new { userId = user.UserId });
            };

            return BadRequest("Password no match");
        }

        public async Task<IActionResult> User(Guid id)
        {
            var user = await _appDbContext.Auth.FirstOrDefaultAsync(x => x.UserId == id);
            var lst = new List<AuthUserModel>() { };
            if (user is null) return RedirectToAction("Index");

            if (user.Role == "admin")
            {
                lst = await _appDbContext.Auth.ToListAsync();
            }
            return View(new { user, lst });

        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _appDbContext.Auth.FirstOrDefaultAsync(x => x.UserId == id);

            if (user is null) return RedirectToAction("Index");
            return View(user);
        }
        public async Task<IActionResult> Password(Guid id)
        {
            var user = await _appDbContext.Auth.FirstOrDefaultAsync(x => x.UserId == id);

            if (user is null) return RedirectToAction("Index");
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(Guid id, RequestPasswordChangeModel reqModel)
        {
            Console.WriteLine(id);
            Console.WriteLine(reqModel);

            var user = await _appDbContext.Auth.FirstOrDefaultAsync(x => x.UserId == id);

            Console.WriteLine(user);

            if (user is null) return NotFound();

            if (user.Password != reqModel.Old) return BadRequest(Json(new { message = "Old password incorrect"}));


            user.Password = reqModel.New;

            int res = await _appDbContext.SaveChangesAsync();
            if(res == 0) return BadRequest();

            return Json(new { message = "Updating success"});
        }

        [HttpPost]
        public async Task<IActionResult> Promote(Guid reqId)
        {
            var user = await _appDbContext.Auth.FirstOrDefaultAsync(x => x.UserId == reqId);

            if (user is null) return NotFound();

            user.Role = "admin";

            int res = await _appDbContext.SaveChangesAsync();
            if (res == 0) return BadRequest();

            return Json(new { message = "Updating success" });
        }
        [HttpPost]
        public async Task<IActionResult> Password(Guid id, AuthUserModel reqModel)
        {
            Console.WriteLine(id);
            Console.WriteLine(reqModel);

            var user = await _appDbContext.Auth.FirstOrDefaultAsync(x => x.UserId == id);

            Console.WriteLine(user);

            if (user is null) return NotFound();

            user.Email = reqModel.Email;
            user.UserName = reqModel.UserName;
            user.FullName = reqModel.FullName;

            int res = await _appDbContext.SaveChangesAsync();
            if (res == 0) return BadRequest();

            return Json(new { message = "Updating success" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid reqId)
        {
            Console.WriteLine(reqId);
            var user = await _appDbContext.Auth.FirstOrDefaultAsync(x => x.UserId == reqId);

            if (user is null) return NotFound();

            _appDbContext.Remove(user);
            int res = await _appDbContext.SaveChangesAsync();
            if (res == 0) return BadRequest();

            return Json(new { message = "Deleted user"});

        }

    }
}
