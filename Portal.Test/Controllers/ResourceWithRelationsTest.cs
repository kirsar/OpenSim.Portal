using System.Linq;
using OpenSim.Portal.Controllers;
using OpenSim.Portal.Controllers.Server;
using OpenSim.Portal.Model;
using Xunit;

namespace OpenSim.Portal.Test.Controllers
{
    public class ResourceWithRelationsTest
    {
        [Fact]
        public void EmbedRelations()
        {
            // Arrange
            var serverResource = new ServerResource(CreateServerModel());

            // Act
            serverResource.EmbedRelations(CreateFieldsTree(), new EmbeddedRelationsSchema());

            // Assert
            Assert.Equal(2, serverResource.Simulations.Count);
            Assert.Equal("Sim1", serverResource.Simulations.ElementAt(0).Name);
            Assert.Equal("Sim2", serverResource.Simulations.ElementAt(1).Name);
            Assert.Equal("User", serverResource.Author.Name);
        }

        [Fact]
        public void EmbedTwoLevelRelations()
        {
            // Arrange
            var serverResource = new ServerResource(CreateServerModel());

            // Act
            serverResource.EmbedRelations(CreateTwoLevelFieldsTree(), new EmbeddedRelationsSchema());

            // Assert
            Assert.Equal(2, serverResource.Simulations.Count());
            Assert.Equal("User1", serverResource.Simulations.ElementAt(0).Author.Name);
            Assert.Equal("User2", serverResource.Simulations.ElementAt(1).Author.Name);
        }

        private static Server CreateServerModel()
        {
            var simulation1 = new Simulation { Name = "Sim1", Author = new User("User1", string.Empty) };
            var simulation2 = new Simulation { Name = "Sim2", Author = new User("User2", string.Empty) };
            var author = new User("User", string.Empty);

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
            .UnfoldFieldsTree()
            .Single();
        }

        private FieldsTreeNode CreateTwoLevelFieldsTree()
        {
            return new[]
                {
                    "_embedded/simulations/name",
                    "_embedded/simulations/_embedded/author/name",
                    "_embedded/author/name",
                }
                .Select(f => f.Split('/'))
                .UnfoldFieldsTree()
                .Single();
        }
    }
}
