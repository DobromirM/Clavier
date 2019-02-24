using System.Collections.Generic;

namespace CLAVIER.Infrastructure.Commands.CodeEditor
{
  public class CodeEditorUser
  {
      public string ConnectionId { get; }
      public string GroupName { get; set; }
      public CodeEditorRole Role { get; set; }
      public bool WantsChange { get; set; }
    
      public CodeEditorUser(string connectionId)
      {
          this.ConnectionId = connectionId;
          this.WantsChange = false;
      }

      public void SwitchRole()
      {
          if (this.Role == CodeEditorRole.Driver)
          {
              this.Role = CodeEditorRole.Navigator;
          }
          else
          {
              this.Role = CodeEditorRole.Driver;
          }
      }
  }
}
