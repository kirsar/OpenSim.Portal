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

        public SimulationsController(ISimulationRepository simulationRepo, IUserRepository usersRepo)
        {
            this.simulationRepo = simulationRepo;
            this.usersRepo = usersRepo;
        }

        // GET: api/v1/Simulations
        [HttpGet]
        public SimulationCollection Get() => new SimulationCollection(simulationRepo
            .GetAll()
            .Select(simulation => new SimulationResource(simulation)
            .EmbedRelations(simulation, Request)).ToList());

        // GET: api/v1/Simulations/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var simulation = simulationRepo.Get(id);

            if (simulation == null)
                return NotFound();

            return new ObjectResult(new SimulationResource(simulation).EmbedRelations(simulation, Request));
        }

        // GET: api/v1/Simulations/5/references
        [HttpGet("{id}/references")]
        public SimulationCollection GetReferences(long id)
        {
            var simulation = simulationRepo.Get(id);

            // TODO handle it properly
            //if (simulation == null)
            //    return NotFound();

            return new SimulationCollection(simulation.References
                .Select(reference => new SimulationResource(reference)
                .EmbedRelations(reference, Request)).ToList());
        }

        // POST: api/v1/Simulations
        [HttpPost]
        public IActionResult Post([FromBody]JObject json)
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

        // PUT: api/v1/Simulations/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Simulation server)
        {
            if (server == null || server.Id != id)
                return BadRequest();
        
            var todo = simulationRepo.Get(id);
            if (todo == null)
                return NotFound();
        
            simulationRepo.Update(new Simulation
            {
                Id = server.Id,
                // TODO
            });

            return new NoContentResult();
        }

        // DELETE: api/ApiWithAction/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (simulationRepo.Get(id) == null)
                return NotFound();
            
            simulationRepo.Remove(id);
            return new NoContentResult();
        }
    }
}
