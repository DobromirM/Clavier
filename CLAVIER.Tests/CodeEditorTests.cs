using CLAVIER.Common.File;
using CLAVIER.Infrastructure.Commands;
using CLAVIER.Infrastructure.Commands.CodeEditor;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CLAVIER.Tests
{
    public class CodeEditorTests : BaseTest
    {
        private Dictionary<string, IEnumerable<string>> _mockFiles = new Dictionary<string, IEnumerable<string>>();

        protected override void RegisterDependencies(IServiceCollection source)
        {
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem.Setup(mfs => mfs.SaveCodeAsync(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()))
                .Returns((string id, IEnumerable<string> lines) =>
                {
                    if (_mockFiles.ContainsKey(id))
                    {
                        _mockFiles[id] = lines;
                    }
                    else
                    {
                        _mockFiles.Add(id, lines);
                    }

                    return Task.CompletedTask;
                });

            source.AddSingleton(mockFileSystem.Object);

            base.RegisterDependencies(source);
        }

        [Fact]
        public async Task ReturnTrueGivenLineUpdateCorrectly()
        {
            var commandDispatcher = _serviceProvider.GetRequiredService<ICommandDispatcher>();

            var id = "1";
            var lines = new List<string>
            {
                "test 1",
            };

            var result = await commandDispatcher.DispatchAsync(new UpdateCodeCommand(id, lines));

            if (!result.Success)
            {
                Assert.True(false);
                return;
            }

            if (_mockFiles.TryGetValue(id, out var updatedLines))
            {
                Assert.True(lines.SequenceEqual(updatedLines));
                return;
            }

            Assert.True(false);
            return;
        }

        [Fact]
        public async Task ReturnTrueGivenMultipleLinesUpdateCorrectly()
        {
            var commandDispatcher = _serviceProvider.GetRequiredService<ICommandDispatcher>();

            var id = "1";
            var lines = new List<string>
            {
                "test 1",
                "test 2",
                "test 3",
                 "test 4"
            };

            var result = await commandDispatcher.DispatchAsync(new UpdateCodeCommand(id, lines));

            if (!result.Success)
            {
                Assert.True(false);
                return;
            }

            if (_mockFiles.TryGetValue(id, out var updatedLines))
            {
                Assert.True(lines.SequenceEqual(updatedLines));
                return;
            }

            Assert.True(false);
            return;
        }

        [Fact]
        public async Task ReturnTrueGivenZeroLinesUpdateCorrectly()
        {
            var commandDispatcher = _serviceProvider.GetRequiredService<ICommandDispatcher>();

            var id = "1";
            var lines = new List<string>
            {
               
            };

            var result = await commandDispatcher.DispatchAsync(new UpdateCodeCommand(id, lines));

            if (!result.Success)
            {
                Assert.True(false);
                return;
            }

            if (_mockFiles.TryGetValue(id, out var updatedLines))
            {
                Assert.True(lines.SequenceEqual(updatedLines));
                return;
            }

            Assert.True(false);
            return;
        }
    }
}
