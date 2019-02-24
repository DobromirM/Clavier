using CLAVIER.Communication;
using System.Linq;
using CLAVIER.Infrastructure.Commands.CodeEditor;
using Xunit;

namespace CLAVIER.Tests
{
    public class ConnectionMappingTests : BaseTest
    {

        [Fact]
        public void ConnectionMappingAddSingleTest()
        {
            ConnectionMapping<string> groups = new ConnectionMapping<string>();
            string group = "group_1";
            CodeEditorUser user = new CodeEditorUser("g1_connection_1");

            groups.Add(group, user);

            Assert.True(groups.Count == 1);
            Assert.Single(groups.GetConnections(group));
            Assert.Equal(user, groups.GetConnections(group).ElementAtOrDefault(0));
            return;
        }

        [Fact]
        public void ConnectionMappingAddMultipleTest()
        {
            ConnectionMapping<string> groups = new ConnectionMapping<string>();
            string firstGroup = "group_1";
            CodeEditorUser g1FirstUser = new CodeEditorUser("g1_connection_1");
            CodeEditorUser g1SecondUser= new CodeEditorUser("g1_connection_2");
            string secondGroup = "group_2";
            CodeEditorUser g2FirstUser = new CodeEditorUser("g2_connection_1");

            groups.Add(firstGroup, g1FirstUser);
            groups.Add(firstGroup, g1SecondUser);
            groups.Add(secondGroup, g2FirstUser);
            Assert.True(groups.Count == 2);
            Assert.Equal(2, groups.GetConnections(firstGroup).ToList<CodeEditorUser>().Count);
            Assert.Equal(g1FirstUser, groups.GetConnections(firstGroup).ElementAtOrDefault(0));
            Assert.Equal(g1SecondUser, groups.GetConnections(firstGroup).ElementAtOrDefault(1));
            Assert.Equal(g2FirstUser, groups.GetConnections(secondGroup).ElementAtOrDefault(0));
            return;
        }

        [Fact]
        public void ConnectionMappingRemoveTest()
        {
            ConnectionMapping<string> groups = new ConnectionMapping<string>();
            string firstGroup = "group_1";
            CodeEditorUser g1FirstUser = new CodeEditorUser("g1_connection_1");
            CodeEditorUser g1SecondUser = new CodeEditorUser("g1_connection2");
            
            groups.Add(firstGroup, g1FirstUser);
            groups.Add(firstGroup, g1SecondUser);
           
            Assert.True(groups.Count == 1);
            Assert.Equal(2, groups.GetConnections(firstGroup).ToList<CodeEditorUser>().Count);
            Assert.Equal(g1FirstUser, groups.GetConnections(firstGroup).ElementAtOrDefault(0));
            Assert.Equal(g1SecondUser, groups.GetConnections(firstGroup).ElementAtOrDefault(1));

            groups.Remove(firstGroup, "g1_connection_1");

            Assert.True(groups.Count == 1);
            Assert.Single(groups.GetConnections(firstGroup).ToList<CodeEditorUser>());
            Assert.Equal(g1SecondUser, groups.GetConnections(firstGroup).ElementAtOrDefault(0));
            return;
        }

        [Fact]
        public void ConnectionMappingFindKeysTest()
        {
            ConnectionMapping<string> groups = new ConnectionMapping<string>();
            string firstGroup = "group_1";
            CodeEditorUser g1FirstUser = new CodeEditorUser("g1_connection_1");
            g1FirstUser.GroupName = firstGroup;
            CodeEditorUser g1SecondUser = new CodeEditorUser("g1_connection_2");
            g1SecondUser.GroupName = firstGroup;
            string secondGroup = "group_2";
            CodeEditorUser g2FirstUser = new CodeEditorUser("g2_connection_1");
            g2FirstUser.GroupName = secondGroup;
            
            groups.Add(firstGroup, g1FirstUser);
            groups.Add(firstGroup, g1SecondUser);
            groups.Add(secondGroup, g2FirstUser);

            Assert.Equal(firstGroup, groups.FindKeys("g1_connection_1").ElementAtOrDefault(0));
            Assert.Equal(firstGroup, groups.FindKeys("g1_connection_2").ElementAtOrDefault(0));
            Assert.Equal(secondGroup, groups.FindKeys("g2_connection_1").ElementAtOrDefault(0));
            return;
        }
    }
}
