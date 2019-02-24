using System.Collections.Generic;
using System.Linq;
using CLAVIER.Infrastructure.Commands.CodeEditor;

namespace CLAVIER.Communication
{
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<CodeEditorUser>> _connections =
            new Dictionary<T, HashSet<CodeEditorUser>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, CodeEditorUser user)
        {
            lock (_connections)
            {
                HashSet<CodeEditorUser> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<CodeEditorUser>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(user);
                }
            }
        }

        public IEnumerable<CodeEditorUser> GetConnections(T key)
        {
            HashSet<CodeEditorUser> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<CodeEditorUser>();
        }

        public IEnumerable<string> FindKeys(string connectionId)
        {
            List<string> groups = new List<string>();
            foreach (var keyValuePair in _connections)
            {
                foreach (var editorUser in keyValuePair.Value)
                {
                    if (editorUser.ConnectionId == connectionId)
                    {
                        groups.Add(editorUser.GroupName);
                    }
                }
            }

            return groups;
        }

        public CodeEditorUser getUser(string connectionId)
        {
            CodeEditorUser user = null;
            foreach (var keyValuePair in _connections)
            {
                foreach (var editorUser in keyValuePair.Value)
                {
                    if (editorUser.ConnectionId == connectionId)
                    {
                        user = editorUser;
                    }
                }
            }

            return user;
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<CodeEditorUser> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    CodeEditorUser user = null;
                    foreach (var keyValuePair in _connections)
                    {
                        foreach (var editorUser in keyValuePair.Value)
                        {
                            if (editorUser.ConnectionId == connectionId)
                            {
                                user = editorUser;
                            }
                        }
                    }
                    
                    connections.Remove(user);
                    
                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }
}
