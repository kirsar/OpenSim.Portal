using System.Linq;
using OpenSim.WebServer.App.Controllers;
using OpenSim.WebServer.Controllers;
using OpenSim.WebServer.Model;
using Xunit;

namespace OpenSim.WebServer.App.Test.Controllers
{
    public class ResourceWithRelationsTest
    {
        [Fact]
        public void EmbedRelations()
        {
            // Arrange
            var serverResource = new ServerResource(CreateServerModel());

            // Act
            serverResource.EmbedRelations(CreateFieldsTree());

            // Assert
            Assert.Equal(2, serverResource.Simulations.Count());
            Assert.Equal("Sim1", serverResource.Simulations.ElementAt(0).Name);
            Assert.Equal("Sim2", serverResource.Simulations.ElementAt(1).Name);
            Assert.Equal("User", serverResource.Author.Name);
        }

        private static Server CreateServerModel()
        {
            var simulation1 = new Simulation { Name = "Sim1" };
            var simulation2 = new Simulation { Name = "Sim2" };
            var author = new User { Name = "User" };

            var server = new Server
            {
                Name = "Server",
                Simulations = new[] { simulation1, simulation2 },
                Author = author
            };
            return server;
        }

        private FieldsTreeNode CreateFieldsTree()
        {
            return new[]
            {
                "_embedded/simulations/name",
                "_embedded/author/name",
            }
            .Select(f => f.Split('/'))
            .UnfoldFieldsTree();
        }
    }
}
