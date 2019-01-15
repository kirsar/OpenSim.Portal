using System.Collections.Generic;
using System.Linq;
using OpenSim.Portal.Controllers;
using Xunit;

namespace OpenSim.Portal.Test.Controllers
{
    public class PartialResponseFieldsExtensionsTest
    {
        [Fact]
        public void UnfoldFieldsTree()
        {
            // Arrange
            var fields = BuildFields();

            // Act
            var tree = fields.UnfoldFieldsTree();

            // Assert
            Assert.Equal("_embedded", tree.Single().Value);
            Assert.Single(tree.Single().Nodes);

            var server = tree.Single().Nodes.Single();
            Assert.Equal(5, server.Nodes.Count());

            var serverEmbedded = server.Nodes.Single(n => n.Value == "_embedded");
            Assert.Equal(3, serverEmbedded.Nodes.Count());

            var simulations = serverEmbedded.Nodes.Single(n => n.Value == "simulations");
            Assert.Equal(4, simulations.Nodes.Count());

            var name = simulations.Nodes.Single(n => n.Value == "name");
            Assert.Empty(name.Nodes);
        }

        [Fact]
        public void GetNodeByPath()
        {
            // Arrange 
            var fields = BuildFields();

            // Act 
            var tree = fields.UnfoldFieldsTree();
            var node = tree.Single().GetByPath("_embedded/servers/_embedded/presentations");

            // Assert
            Assert.Equal("presentations", node.Value);
        }

        [Fact]
        public void GetLeafNodeByPath()
        {
            // Arrange 
            var fields = BuildFields();

            // Act 
            var tree = fields.UnfoldFieldsTree();
            var node = tree.Single().GetByPath("_embedded/servers/_embedded/author/_links");

            // Assert
            Assert.Equal("_links", node.Value);
        }

        private static IEnumerable<IEnumerable<string>> BuildFields() =>
            new[]
            {
                "_embedded/servers/name",
                "_embedded/servers/description",
                "_embedded/servers/id",
                "_embedded/servers/_links",
                "_embedded/servers/_embedded/simulations/name",
                "_embedded/servers/_embedded/simulations/description",
                "_embedded/servers/_embedded/simulations/id",
                "_embedded/servers/_embedded/simulations/_links",
                "_embedded/servers/_embedded/presentations/name",
                "_embedded/servers/_embedded/presentations/description",
                "_embedded/servers/_embedded/presentations/id",
                "_embedded/servers/_embedded/presentations/_links",
                "_embedded/servers/_embedded/author/name",
                "_embedded/servers/_embedded/author/id",
                "_embedded/servers/_embedded/author/_links"
            }.Select(f => f.Split('/'));
    }
}
