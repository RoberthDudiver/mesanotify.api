using Elastic.CommonSchema;
using Microsoft.AspNetCore.SignalR;


namespace App.Api.Hubs
{
    public class CallsHub : Hub
    {
        public async Task SendCallUpdate(string message)
        {
            var guid = Guid.NewGuid().ToString();
            await Clients.All.SendAsync("receivecallupdate", guid, message);
        }
    }
}
