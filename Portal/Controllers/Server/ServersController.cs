using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenSim.Portal.Controllers.Presentation;
using OpenSim.Portal.Controllers.Simulation;
using OpenSim.Portal.Model;

namespace OpenSim.Portal.Controllers.Server
{
    [ApiVersion("1.0")]
    [Produces("application/hal+json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ServersController : Controller
    {
        private readonly IServerRepository serversRepo;
        private readonly ISimulationRepository simulationsRepo;
        private readonly IPresentationRepository presentationsRepo;
        private readonly UserManager<Model.User> userManager;
        private readonly IEmbeddedRelationsSchema embeddedRelationsSchema;

        public ServersController(
            IServerRepository serversRepo,
            ISimulationRepository simulationsRepo,
            IPresentationRepository presentationsRepo,
            UserManager<Model.User> userManager,
            IEmbeddedRelationsSchema embeddedRelationsSchema)
        {
            this.serversRepo = serversRepo;
            this.simulationsRepo = simulationsRepo;
            this.presentationsRepo = presentationsRepo;
            this.userManager = userManager;
            this.embeddedRelationsSchema = embeddedRelationsSchema;
        }

        // GET: api/v1/Servers
        [HttpGet]
        public ServerCollection Get() =>
            new ServerCollection(LinkTemplates.Servers.Get.Rel,
                    serversRepo.GetAll().Select(server => new ServerResource(server)))
                .EmbedRelations(Request, embeddedRelationsSchema, userManager);

        // GET: api/v1/Servers/5
        [HttpGet("{id}")]
        public ActionResult<ServerResource> Get(int id)
        {
            var server = serversRepo.Get(id);

            if (server == null)
                return NotFound();

            return new ServerResource(server).EmbedRelations(Request, embeddedRelationsSchema, userManager);
        }

        // GET: api/v1/Servers/5/simulations
        [HttpGet("{id}/simulations")]
        public ActionResult<SimulationCollection> GetSimulations(int id)
        {
            var server = serversRepo.Get(id);

            if (server == null)
                return NotFound();

            return new SimulationCollection(LinkTemplates.Servers.GetSimulations.Rel,
                    server.Simulations.Select(simulation => new SimulationResource(simulation)))
                .EmbedRelations(Request, embeddedRelationsSchema, userManager);
        }

        // GET: api/v1/Servers/5/presentations
        [HttpGet("{id}/presentations")]
        public ActionResult<PresentationCollection> GetPresentations(int id)
        {
            var server = serversRepo.Get(id);

            if (server == null)
                return NotFound();

            return new PresentationCollection(LinkTemplates.Servers.GetPresentations.Rel,
                    server.Presentations.Select(presentation => new PresentationResource(presentation)))
                .EmbedRelations(Request, embeddedRelationsSchema, userManager);
        }

        // POST: api/v1/Servers
        [HttpPost]
        [Authorize]
        public ActionResult<ServerResource> Post([FromBody] ServerResource serverResource) =>
            serverResource != null ? Get(AddToRepo(serverResource)) : BadRequest();

        // TODO handle errors
        private int AddToRepo(ServerResource serverResource)
        {
            var server = new Model.Server
            {
                Name = serverResource.Name,
                Description = serverResource.Description,
                AuthorId = userManager.Users.First().Id // TODO current user
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
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var sever = serversRepo.Get(id);
        //    if (sever == null)
        //        return NotFound();

        //    serversRepo.Remove(id);
        //    return new NoContentResult();
        //}
    }
}
