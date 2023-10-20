using Microsoft.AspNetCore.SignalR;
using WebSocialNet.Domain.DTOs.ChatDTOs;
using WebSocialNet.Domain.Interfaces.IServices;

namespace WebSocialNet.Service
{

    public sealed class ChatHubService : Hub<IChatHubService>
    {

        private readonly string _botUser;
        private readonly IDictionary<string, UserConnection> _connections;

        public ChatHubService(IDictionary<string, UserConnection> connections)
        {
            _botUser = "Chat Bot";
            _connections = connections;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId} has joined");
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                _connections.Remove(Context.ConnectionId);
                Clients.Group(userConnection.Room).ReceiveMessage(_botUser, $"{userConnection.User} has left");
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGroup(UserConnection userConnection)
        {
            
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);
            _connections[Context.ConnectionId] = userConnection;

            await Clients.Group(userConnection.Room).ReceiveMessage(_botUser, $"{userConnection.User} has joined {userConnection.Room}");
        }

        // TODO PRIVATE MESSAGES
        public async Task SendPrivateMessage(string user, string message)
        {
            Console.WriteLine($"{Context.ConnectionId} is sending a private message to {user}: {message}");
            await Clients.Client(user).ReceiveMessage(message);
        }

        public async Task SendMessageInGroup(string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                await Clients.Group(userConnection.Room).ReceiveMessage(userConnection.User, message);
            }
        }

    }
}




    //{"protocol":"json","version":1} to connect

    //{"arguments":["Hello how u doing"],"target":"SendMessage","type":1} to send messages

    //{"arguments":["USERID" ,"Hello user!!"],"target":"SendPrivateMessage","type":1} to send private messages
