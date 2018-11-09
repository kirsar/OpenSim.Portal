using OpenSim.WebServer.App.Controllers;
using Xunit;

namespace OpenSim.WebServer.App.Test
{
    public class PartialResponseFieldsExtensionsTest
    {
        [Fact]
        public void TestUnfoldFieldsTree()
        {
            // Arrange
            var fields = new[]
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
            };

            // Act
            var tree = fields.UnfoldFieldsTree();

            // Assert
        }
    }
}
