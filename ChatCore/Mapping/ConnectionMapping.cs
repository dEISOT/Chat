namespace ChatCore.Mapping
{
    public static class ConnectionMapping
    {

        private static readonly Dictionary<Guid, HashSet<string>> _connections =
           new Dictionary<Guid, HashSet<string>>();

        public static int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public static void Add(Guid userId, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(userId, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(userId, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }
        public static bool TryGetValue(Guid userId, out HashSet<string> connectionId)
        {
            lock (_connections)
            {
                return _connections.TryGetValue(userId, out connectionId);
            }
        }
        public static IEnumerable<string> GetConnections(Guid userId)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(userId, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public static void Remove(Guid userId, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(userId, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(userId);
                    }
                }
            }
        }
    }
}

