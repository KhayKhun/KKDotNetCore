using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KKDotNetCore.MvcApp.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerManager _logger;
        public UserController(AppDbContext appDbContext, ILoggerManager logger) {
            _appDbContext = appDbContext; 
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var lst = await _appDbContext.User
                //.Where(x => x.DeleteFlag == false)
                .AsNoTracking()
                .OrderByDescending(x => x.UserId)
                .ToListAsync();

            _logger.LogInfo("Index: response View.");
            return View(lst);
        }
        public async Task<IActionResult> List(int pageNo=1, int pageSize = 10)
        {
            var query = _appDbContext.User
                //.Where(x => x.DeleteFlag == false)
                .AsNoTracking()
                .OrderByDescending(x => x.UserId);

            var lst = await query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var rowCount = await query.CountAsync();
            var pageCount = rowCount / pageSize;
            
            if(rowCount % pageSize > 0)
            {
                pageCount++; 
            }
            UserResponseModel model = new UserResponseModel() {
                Data = lst,
                PageSetting = new PageSettingModel(pageNo,pageSize,pageCount,rowCount, "/user/list")
            };

            _logger.LogInfo("List: response View.");
            return View("UserList", model);
        }
        public IActionResult Create()
        {
            _logger.LogInfo("Create: response View.");
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Save(UserDataModel reqModel)
        {
            await _appDbContext.User.AddAsync(reqModel);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInfo("Save: response View.");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            var user = await _appDbContext.User.FirstOrDefaultAsync(x => x.UserId == id);

            if (user is null)
            {
                _logger.LogDebug($"EditAsync: user with userId={id} is null.");
                return RedirectToAction("Index");
            }

            _logger.LogDebug($"Edit: Edited user with userId={id}.");
            _logger.LogInfo("EditAsync: response View.");
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id,UserDataModel reqModel)
        {

            var user = await _appDbContext.User.FirstOrDefaultAsync(x => x.UserId == id);

            if (user is null)
            {
                _logger.LogDebug($"EditAsync: user with userId={id} is null.");
                return RedirectToAction("Index");
            }
            user.UserEmail = reqModel.UserEmail;
            user.UserPhone = reqModel.UserPhone;
            user.UserAddress = reqModel.UserAddress;
            user.UserName = reqModel.UserName;

            await _appDbContext.SaveChangesAsync();

            _logger.LogDebug($"Update: updated user with userId={id}.");
            _logger.LogInfo("Update: response View.");
            return RedirectToAction("Index");
        }

        public async  Task<IActionResult> Delete(int id)
        {
            var user = await _appDbContext.User.FirstOrDefaultAsync(x => x.UserId == id);

            if (user is null)
            {
                _logger.LogDebug($"Delete: user with userId={id} is null.");
                return RedirectToAction("Index");
            }

            _appDbContext.Remove(user);
            await _appDbContext.SaveChangesAsync();

            _logger.LogDebug($"Delete: deleted user with userId={id}.");
            _logger.LogInfo("Delete: response View.");
            return RedirectToAction("Index");
        }
    }
}
