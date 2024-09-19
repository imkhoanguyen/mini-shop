using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace API.SignalR
{
    public class PresenceTracker
    {
        private static readonly ConcurrentDictionary<string, List<string>> OnlineUsers =
            new ConcurrentDictionary<string, List<string>>();

        public Task<bool> UserConnected(string username, string connectionId)
        {
            bool isOnline = false;

            OnlineUsers.AddOrUpdate(username, new List<string> { connectionId }, (key, existingList) =>
            {
                existingList.Add(connectionId);
                return existingList;
            });

            if (OnlineUsers[username].Count == 1)
            {
                isOnline = true;
            }

            return Task.FromResult(isOnline);
        }

        public Task<bool> UserDisconnected(string username, string connectionId)
        {
            bool isOffline = false;

            if (OnlineUsers.TryGetValue(username, out var connections))
            {
                connections.Remove(connectionId);

                if (connections.Count == 0)
                {
                    OnlineUsers.TryRemove(username, out _);
                    isOffline = true;
                }
            }

            return Task.FromResult(isOffline);
        }

        public Task<string[]> GetOnlineUsers()
        {
            var users = OnlineUsers.OrderBy(k => k.Key).Select(k => k.Key).ToArray();
            return Task.FromResult(users);
        }

        public static Task<List<string>> GetConnectionsForUser(string username)
        {
            var connectionIds = OnlineUsers.GetValueOrDefault(username) ?? new List<string>();
            return Task.FromResult(connectionIds);
        }
    }
}
