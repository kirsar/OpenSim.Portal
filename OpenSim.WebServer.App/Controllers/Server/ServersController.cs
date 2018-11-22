using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using OpenSim.WebServer.App.Controllers;
using OpenSim.WebServer.Model;
using WebApi.Hal;

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
        private readonly IEmbeddedRelationsSchema embeddedRelationsSchema;

        public ServersController(
            IServerRepository serversRepo, 
            ISimulationRepository simulationsRepo,
            IPresentationRepository presentationsRepo,
            IUserRepository usersRepo,
            IEmbeddedRelationsSchema embeddedRelationsSchema)
        {
            this.serversRepo = serversRepo;
            this.simulationsRepo = simulationsRepo;
            this.presentationsRepo = presentationsRepo;
            this.usersRepo = usersRepo;
            this.embeddedRelationsSchema = embeddedRelationsSchema;
        }

        // GET: api/v1/Servers
        [HttpGet]
        public ServerCollection Get()
        {
            return new ServerCollection(serversRepo
                .GetAll()
                .Select(server => new ServerResource(server))
                .ToList()
                .EmbedRelations(Request, embeddedRelationsSchema));
        }

        // GET: api/v1/Servers/5
        [HttpGet("{id}")]
        public ActionResult<ServerResource> Get(long id)
        {
            var server = serversRepo.Get(id);

            if (server == null)
                return NotFound();

            return new ServerResource(server).EmbedRelations(Request, embeddedRelationsSchema);
        }

        // GET: api/v1/Servers/5/simulations
        [HttpGet("{id}/simulations")]
        public SimulationCollection GetSimulations(long id)
        {
            var server = serversRepo.Get(id);

            // TODO handle it properly
            //if (simulation == null)
            //    return NotFound();

            if (server.Simulations == null)
                return new SimulationCollection(Enumerable.Empty<SimulationResource>().ToList());

            return new SimulationCollection(server.Simulations
                .Select(simulation => new SimulationResource(simulation)
                    .EmbedRelations(Request, embeddedRelationsSchema)).ToList());
        }

        // GET: api/v1/Servers/5/presentations
        [HttpGet("{id}/presentations")]
        public PresentationCollection GetPresentations(long id)
        {
            var server = serversRepo.Get(id);

            // TODO handle it properly
            //if (simulation == null)
            //    return NotFound();

            if (server.Presentations == null)
                return new PresentationCollection(Enumerable.Empty<PresentationResource>().ToList());

            return new PresentationCollection(server.Presentations
                .Select(presentation => new PresentationResource(presentation)
                    .EmbedRelations(Request, embeddedRelationsSchema)).ToList());
        }

        // POST: api/v1/Servers
        [HttpPost]
        public ActionResult<ServerResource> Post([FromBody]ServerResource serverResource)
        {
            if (serverResource == null)
                return BadRequest();

            //return CreatedAtRoute("Get", new { id = id }, server);
            return Get(AddToRepo(serverResource));
        }

        // TODO handle errors
        private long AddToRepo(ServerResource serverResource)
        {
            var server = new Server
            {
                Name = serverResource.Name,
                Description = serverResource.Description,
                Author = usersRepo.GetAll().First()       // TODO current user
            };

            foreach (var link in serverResource.Links.Where(l => l.Rel == LinkTemplates.Servers.GetSimulations.Rel))
                server.AddSimulation(simulationsRepo.Get(link.GetId()));
           
            foreach (var link in serverResource.Links.Where(l => l.Rel == LinkTemplates.Servers.GetPresentations.Rel))
                server.AddPresentation(presentationsRepo.Get(link.GetId()));

            serversRepo.Add(server);
            return server.Id;
        }

        // PUT: api/v1/Servers/5
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody]ServerResource server)
        //{
        //    if (server == null || server.Id != id)
        //        return BadRequest();

        //    var todo = serversRepo.Get(id);
        //    if (todo == null)
        //        return NotFound();

        //    serversRepo.Update(new Server
        //    {
        //        Id = server.Id,
        //        // TODO
        //    });

        //    return new NoContentResult();
        //}

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
