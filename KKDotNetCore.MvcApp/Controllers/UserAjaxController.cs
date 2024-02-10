using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KKDotNetCore.MvcApp.Controllers
{
    public class UserAjaxController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public UserAjaxController(AppDbContext appDbContext) {  _appDbContext = appDbContext; }

        public async Task<IActionResult> Index()
        {
            var lst = await _appDbContext.User.OrderByDescending(x => x.UserId).ToListAsync();
            return View(lst);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(UserDataModel reqModel)
        {
            Console.WriteLine(reqModel.UserName is null);
            Console.WriteLine(reqModel.UserEmail is null);
            Console.WriteLine(reqModel.UserPhone is null);
            Console.WriteLine(reqModel.UserAddress is null);


            await _appDbContext.User.AddAsync(reqModel);
            int res = await _appDbContext.SaveChangesAsync();
            return Json(new { Message = res > 0 ? "Saving success" : "Saving failed" });
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            var user = await _appDbContext.User.FirstOrDefaultAsync(x => x.UserId == id);

            if (user is null) return RedirectToAction("Index");

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id,UserDataModel reqModel)
        {
            Console.WriteLine(id);
            Console.WriteLine(reqModel);

            var user = await _appDbContext.User.FirstOrDefaultAsync(x => x.UserId == id);

            Console.WriteLine(user);

            if (user is null) return Json(new { Message = "no data found" });

            user.UserEmail = reqModel.UserEmail;
            user.UserPhone = reqModel.UserPhone;
            user.UserAddress = reqModel.UserAddress;
            user.UserName = reqModel.UserName;

            int res = await _appDbContext.SaveChangesAsync();
            return Json(new { Message = res > 0 ? "Updating success" : "Updating failed" });
        }

        [HttpPost]
        public async  Task<IActionResult> Delete(UserDataModel reqModel)
        {
            Console.WriteLine(reqModel.UserId);
            var user = await _appDbContext.User.FirstOrDefaultAsync(x => x.UserId == reqModel.UserId);

            if (user is null) return Json(new { Message = "no data found" });

            _appDbContext.Remove(user);
            int res = await _appDbContext.SaveChangesAsync();
            return Json(new { Message = res > 0 ? "Deleting success" : "Deleting failed" });

        }
    }
}
