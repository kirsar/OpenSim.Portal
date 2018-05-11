using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/hal+json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SimulationsController : Controller
    {
        private readonly ISimulationRepository repo;

        public SimulationsController(ISimulationRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/v1/Simulations
        [HttpGet]
        public SimulationCollection Get() => new SimulationCollection(repo
            .GetAll()
            .Select(simulation => new SimulationResource(simulation)
            .EmbedRelations(simulation, Request)).ToList());

        // GET: api/v1/Simulations/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var simulation = repo.Get(id);

            if (simulation == null)
                return NotFound();

            return new ObjectResult(new SimulationResource(simulation).EmbedRelations(simulation, Request));
        }

        // POST: api/v1/Simulations
        [HttpPost]
        public IActionResult Post([FromBody]SimulationResource simulation)
        {
            if (simulation == null)
                return BadRequest();

            repo.Add(new Simulation
            {
                Id = simulation.Id,
                // TODO
            });

            return CreatedAtRoute("Get", new { id = simulation.Id }, simulation);
        }

        // PUT: api/v1/Simulations/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Simulation server)
        {
            if (server == null || server.Id != id)
                return BadRequest();
        
            var todo = repo.Get(id);
            if (todo == null)
                return NotFound();
        
            repo.Update(new Simulation
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
            if (repo.Get(id) == null)
                return NotFound();
            
            repo.Remove(id);
            return new NoContentResult();
        }
    }
}
