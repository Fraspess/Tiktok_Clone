using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace Tiktok_Clone.BLL.Services.ConnectionManager
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly ConcurrentDictionary<Guid, WebSocket> _connections = new();
        public void Add(Guid userId, WebSocket socket)
        {
            _connections[userId] = socket;
        }

        public WebSocket? Get(Guid userId)
        {
            return _connections.GetValueOrDefault(userId);
        }

        public bool IsOnline(Guid userId)
        {
            return _connections.ContainsKey(userId)
                && _connections[userId].State == WebSocketState.Open;
        }

        public void Remove(Guid userId)
        {
            _connections.TryRemove(userId, out var _);
        }

        public Task SendAsync(Guid userId, string text)
        {
            throw new NotImplementedException();
        }
    }
}
