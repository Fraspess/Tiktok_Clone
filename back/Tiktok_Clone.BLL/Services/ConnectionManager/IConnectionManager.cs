using System.Net.WebSockets;

namespace Tiktok_Clone.BLL.Services.ConnectionManager
{
    public interface IConnectionManager
    {
        void Add(Guid userId, WebSocket socket);
        void Remove(Guid userId);
        bool IsOnline(Guid userId);
        Task SendAsync(Guid userId, string text);

        public WebSocket? Get(Guid userId);
    }
}
