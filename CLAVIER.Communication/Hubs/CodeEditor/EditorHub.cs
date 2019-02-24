using CLAVIER.Infrastructure.Commands;
using CLAVIER.Infrastructure.Commands.CodeEditor;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CLAVIER.Infrastructure.Queries;

namespace CLAVIER.Communication.Hubs.CodeEditor
{
    internal class EditorHub : Hub<IEditorClient>
    {
        private ICommandDispatcher _commandDispatcher;
        private IQueryDispatcher _queryDispatcher;
        private Guid guid = Guid.NewGuid();
        protected readonly static ConnectionMapping<string> _groups = new ConnectionMapping<string>();

        public EditorHub(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }
        
        public async override Task OnConnectedAsync()
        {
            string group = Context.GetHttpContext().Request.Query["group"];
            CodeEditorUser user = new CodeEditorUser(Context.ConnectionId);
            
            if (string.IsNullOrEmpty(group) || group == "null")
            {
                string groupName = Convert.ToBase64String(guid.ToByteArray());
                groupName = groupName.Replace("=","");
                groupName = groupName.Replace("+","");
                groupName = groupName.Replace("/","");

                user.Role = CodeEditorRole.Driver;
                user.GroupName = groupName;


                _groups.Add(groupName, user);
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                await Clients.Client(Context.ConnectionId).ReceiveGroup(groupName);
            }
            else
            {
                user.GroupName = group;
                
                if (CanJoinGroup(group))
                {
                    CodeEditorUser partner = _groups.GetConnections(group).First();

                    if (partner.Role == CodeEditorRole.Driver)
                    {
                        user.Role = CodeEditorRole.Navigator;
                    }
                    else
                    {
                        user.Role = CodeEditorRole.Driver;
                    }
                    
                    _groups.Add(group, user);
                    await Groups.AddToGroupAsync(Context.ConnectionId, group);
                }
                else
                {
                    await Clients.Client(Context.ConnectionId).ReceiveError();
                    return;
                }
            }
            
            await Clients.Client(Context.ConnectionId).ReceiveRole(user.Role);
            ReadCodeResult code = await _queryDispatcher.DisptachAsync<ReadCodeQuery, ReadCodeResult>(new ReadCodeQuery(_groups.FindKeys(Context.ConnectionId).First()));
            await Clients.Client(Context.ConnectionId).ReceiveCodeUpdate(code.Lines);
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var groupsToLeave = _groups.FindKeys(Context.ConnectionId);

            foreach(var group in groupsToLeave)
            {
                _groups.Remove(group, Context.ConnectionId);
                Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
                Clients.OthersInGroup(group).PartnerDisconnected();
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task UpdateCode(IEnumerable<string> lines)
        {
            await _commandDispatcher.DispatchAsync(new UpdateCodeCommand(_groups.FindKeys(Context.ConnectionId).First(), lines));

            foreach(var group in _groups.FindKeys(Context.ConnectionId))
            {
                await Clients.Group(group).ReceiveCodeUpdate(lines);
            }
        }

        public async Task UpdateCodeHighlights(int lineNumber)
        {
            foreach (var group in _groups.FindKeys(Context.ConnectionId))
            {
                await Clients.Group(group).ReceiveCodeHighlightsUpdate(lineNumber);
            }
        }

        public async Task UpdateNotes(IEnumerable<string> notes)
        {
            foreach (var group in _groups.FindKeys(Context.ConnectionId))
            {
                await Clients.Group(group).ReceiveNotesUpdate(notes);
            }
        }

        public async Task RequestRoleSwitch()
        {
            CodeEditorUser user = _groups.getUser(Context.ConnectionId);
            user.WantsChange = !user.WantsChange;
            
            string userGroup = _groups.FindKeys(Context.ConnectionId).First();

            int userCount = _groups.GetConnections(userGroup).Count();
            bool swapUsers = _groups.GetConnections(userGroup).All(u => u.WantsChange);
            if (swapUsers && userCount == 2)
            {
                foreach (var currentUser in _groups.GetConnections(userGroup))
                {
                    currentUser.SwitchRole();
                    currentUser.WantsChange = false;
                    await Clients.Client(currentUser.ConnectionId).ReceiveRole(currentUser.Role);
                }
            }
            
            foreach (var group in _groups.FindKeys(Context.ConnectionId))
            {
                await Clients.Group(group).ReceiveSwitchUpdate(user.WantsChange);
            }
        }
        
        private bool CanJoinGroup(string group)
        {
            if (_groups.GetConnections(group).Count() <= 0)
            {
                return false;
            }

            if (_groups.GetConnections(group).Count() > 1)
            {
                return false;
            }

            return true;
        }

    }
}
