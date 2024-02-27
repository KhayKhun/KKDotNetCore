using KKDotNetCore.RealtimeChartMvcApp.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace KKDotNetCore.RealtimeChartMvcApp.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IHubContext<RealtimeHub> _hubContext;

        public TeamController(AppDbContext dbContext, IHubContext<RealtimeHub> hubContext)
        {
            _dbContext = dbContext;
            _hubContext = hubContext;
        }

        [ActionName("Index")]
        public async Task<IActionResult> TeamIndex()
        {
            var lst = await _dbContext.Team.AsNoTracking().ToListAsync();
            return View("Index",lst);
        }

        [ActionName("Create")]
        public IActionResult TeamCreate()
        {
            return View("TeamCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> TeamSave(TeamDataModel reqModel)
        {
            await _dbContext.Team.AddAsync(reqModel);
            await _dbContext.SaveChangesAsync();

            var lst = await _dbContext.Team.AsNoTracking().ToListAsync();

            await _hubContext.Clients.All.SendAsync("ClientRecieveTeam", JsonSerializer.Serialize(lst));

            return Redirect("/Team/Create");
        }
    }
}
