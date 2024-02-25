using KKDotNetCore.RealtimeChartMvcApp.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace KKDotNetCore.RealtimeChartMvcApp.Hubs
{
    public class RealtimeHub : Hub
    {
        public async Task ServerReceiveMessage()
        {

            Random random = new Random();

            var model = new PieChartResourceModel()
            {
                Data = new PieChartModel()
                {
                    Labels = new List<string> { "A", "B", "C", "D" },
                    Series = new List<int> { random.Next(1,50), random.Next(1, 50), random.Next(1, 50), random.Next(1, 50), }
                }
            };

            await Clients.All.SendAsync("ClientReceiveMessage", JsonConvert.SerializeObject(model));
        }
    }
}
