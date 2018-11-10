using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/hal+json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ServersController : Controller
    {
        private readonly IServerRepository serversRepo;
        private readonly ISimulationRepository simulationsRepo;
        private readonly IPresentationRepository presentationsRepo;
        private readonly IUserRepository usersRepo;

        public ServersController(
            IServerRepository serversRepo, 
            ISimulationRepository simulationsRepo,
            IPresentationRepository presentationsRepo,
            IUserRepository usersRepo)
        {
            this.serversRepo = serversRepo;
            this.simulationsRepo = simulationsRepo;
            this.presentationsRepo = presentationsRepo;
            this.usersRepo = usersRepo;
        }

        // GET: api/v1/Servers
        [HttpGet]
        public ServerCollection Get()
        {
            return new ServerCollection(serversRepo
                .GetAll()
                .Select(server => new ServerResource(server))
                .ToList()
                .EmbedRelations(Request));
        }

        // GET: api/v1/Servers/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var server = serversRepo.Get(id);

            if (server == null)
                return NotFound();

            return new ObjectResult(new ServerResource(server).EmbedRelations(Request));
        }
        
        // POST: api/v1/Servers
        [HttpPost]
        public IActionResult Post([FromBody]ServerResource serverResource)
        {
            if (serverResource == null)
                return BadRequest();

            var server = new Server
            {
                Name = serverResource.Name,
                Description = serverResource.Description,
                // TODO current
                Author = usersRepo.GetAll().First()
            };
            
            foreach (var link in serverResource.Links.Where(l => l.Rel == LinkTemplates.Simulations.GetSimulation.Rel))
            {
                // TODO handle error
                // TODO replace this by something smarter
                var id = long.Parse(link.Href.Substring(link.Rel.Length + 2), NumberStyles.Any, CultureInfo.InvariantCulture);
                var simulation = simulationsRepo.Get(id);
                server.AddSimulation(simulation);
            }

            foreach (var link in serverResource.Links.Where(l => l.Rel == LinkTemplates.Presentations.GetPresentation.Rel))
            {
                // TODO handle error
                // TODO replace this by something smarter
                var id = long.Parse(link.Href.Substring(link.Rel.Length + 2), NumberStyles.Any, CultureInfo.InvariantCulture);
                var presentation = presentationsRepo.Get(id);
                server.AddPresentation(presentation);
            }

            serversRepo.Add(server);

            //return CreatedAtRoute("Get", new { id = server.Id }, server);
            return Get(server.Id);
        }
        
        // PUT: api/v1/Servers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ServerResource server)
        {
            if (server == null || server.Id != id)
                return BadRequest();
        
            var todo = serversRepo.Get(id);
            if (todo == null)
                return NotFound();
        
            serversRepo.Update(new Server
            {
                Id = server.Id,
                // TODO
            });

            return new NoContentResult();
        }
        
        // DELETE: api/v1/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sever = serversRepo.Get(id);
            if (sever == null)
                return NotFound();
            
            serversRepo.Remove(id);
            return new NoContentResult();
        }
    }
}
