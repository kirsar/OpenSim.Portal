using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/hal+json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SimulationsController : Controller
    {
        private readonly ISimulationRepository simulationRepo;
        private readonly IUserRepository usersRepo;
        private readonly IEmbeddedRelationsSchema embeddedRelationsSchema;

        public SimulationsController(
            ISimulationRepository simulationRepo, 
            IUserRepository usersRepo, 
            IEmbeddedRelationsSchema embeddedRelationsSchema)
        {
            this.simulationRepo = simulationRepo;
            this.usersRepo = usersRepo;
            this.embeddedRelationsSchema = embeddedRelationsSchema;
        }

        // GET: api/v1/Simulations
        [HttpGet]
        public SimulationCollection Get() => 
            new SimulationCollection(simulationRepo
            .GetAll()
            .Select(simulation => new SimulationResource(simulation))
            .ToList())
            .EmbedRelations(Request, embeddedRelationsSchema);

        // GET: api/v1/Simulations/5
        [HttpGet("{id}")]
        public ActionResult<SimulationResource> Get(long id)
        {
            var simulation = simulationRepo.Get(id);

            if (simulation == null)
                return NotFound();

            return new SimulationResource(simulation).EmbedRelations(Request, embeddedRelationsSchema);
        }

        // GET: api/v1/Simulations/5/references
        [HttpGet("{id}/references")]
        public ActionResult<SimulationCollection> GetReferences(long id)
        {
            var simulation = simulationRepo.Get(id);

            if (simulation == null)
                return NotFound();

            return new SimulationCollection(simulation.References
                .Select(reference => new SimulationResource(reference, LinkTemplates.Simulations.GetReferences.Rel))
                .ToList())
                .EmbedRelations(Request, embeddedRelationsSchema);
        }

        // GET: api/v1/Simulations/5/presentations
        [HttpGet("{id}/presentations")]
        public ActionResult<PresentationCollection> GetPresentations(long id)
        {
            var simulation = simulationRepo.Get(id);

            if (simulation == null)
                return NotFound();

            return new PresentationCollection(simulation.Presentations
                .Select(presentation => new PresentationResource(presentation, LinkTemplates.Simulations.GetPresentations.Rel))
                .ToList())
                .EmbedRelations(Request, embeddedRelationsSchema);
        }

        // POST: api/v1/Simulations
        [HttpPost]
        public ActionResult<SimulationResource> Post([FromBody]JObject json)
        {
            var simulations = simulationRepo.GetAll();

            var simulation = new Simulation
            {
                Name = json["name"].Value<string>(),
                Description = json["description"].Value<string>(),
                Author = usersRepo.GetAll().First(),
                References = json["references"].Select(token => simulations.Single(s => s.Name == token.Value<string>())).ToList()
            };

            simulationRepo.Add(simulation);

            foreach (var reference in simulation.References)
               reference.AddConsumer(simulation);

            return Get(simulation.Id);
        }
    }
}
