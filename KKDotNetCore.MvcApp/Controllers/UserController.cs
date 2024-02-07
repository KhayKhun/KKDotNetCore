using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KKDotNetCore.MvcApp.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public UserController(AppDbContext appDbContext) {  _appDbContext = appDbContext; }

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
        public async  Task<IActionResult> Save(UserDataModel reqModel)
        {
            await _appDbContext.User.AddAsync(reqModel);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
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

            var user = await _appDbContext.User.FirstOrDefaultAsync(x => x.UserId == id);

            if (user is null) return RedirectToAction("Index");
            user.UserEmail = reqModel.UserEmail;
            user.UserPhone = reqModel.UserPhone;
            user.UserAddress = reqModel.UserAddress;
            user.UserName = reqModel.UserName;

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async  Task<IActionResult> Delete(int id)
        {
            var user = await _appDbContext.User.FirstOrDefaultAsync(x => x.UserId == id);

            if (user is null) return RedirectToAction("Index");

            _appDbContext.Remove(user);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
