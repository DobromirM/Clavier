using CLAVIER.Communication.Hubs.CodeEditor;
using Microsoft.AspNetCore.Builder;
using System;

namespace CLAVIER.Communication.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCommunication(this IApplicationBuilder source)
        {
            source.UseSignalR(options =>
            {
                options.MapHub<EditorHub>("/editorhub");
            });

            return source;
        }
    }
}
