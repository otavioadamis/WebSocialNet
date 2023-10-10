using Microsoft.AspNetCore.SignalR;
using WebSocialNet.Domain.Interfaces.IServices;

namespace WebSocialNet.Service
{
    public sealed class ChatHubService : Hub<IChatHubService>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId} has joined");
        }

        public async Task SendPrivateMessage(string user, string message)
        {
            Console.WriteLine($"{Context.ConnectionId} is sending a private message to {user}: {message}");
            await Clients.Client(user).ReceiveMessage(message);
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId} said: {message}");
        }
    }



    //{"protocol":"json","version":1} to connect

    //{"arguments":["Hello how u doing"],"target":"SendMessage","type":1} to send messages

    //{"arguments":["USERID" ,"Hello user!!"],"target":"SendPrivateMessage","type":1} to send private messages
}
