using System.Collections.Generic;
using System.Threading.Tasks;
using CLAVIER.Infrastructure.Commands.CodeEditor;

namespace CLAVIER.Communication.Hubs.CodeEditor
{
    public interface IEditorClient
    {
        Task ReceiveCodeUpdate(IEnumerable<string> lines);

        Task ReceiveNotesUpdate(IEnumerable<string> notes);

        Task ReceiveCodeHighlightsUpdate(int lineNumber);

        Task ReceiveRole(CodeEditorRole role);
        
        Task ReceiveGroup(string group);

        Task PartnerDisconnected();
        
        Task ReceiveError();
        
        Task ReceiveSwitchUpdate(bool state);
    }
}
