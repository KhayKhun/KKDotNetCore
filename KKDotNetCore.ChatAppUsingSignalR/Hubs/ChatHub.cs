using Microsoft.AspNetCore.SignalR;

namespace KKDotNetCore.ChatAppUsingSignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task ServerRecieveMessage(string user, string message)
        {
            await Clients.All.SendAsync("ClientReceiveMessage", user, message);
        }
    }
}