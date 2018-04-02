using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OpenSim.WebServer.App.Controllers.Simulation
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SimulationController : Controller
    {
        private readonly ISimulationRepository repo;

        public SimulationController(ISimulationRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/Simulation
        [HttpGet]
        public IEnumerable<Simulation> Get() => repo.GetAll();

        // GET: api/Simulation/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var server = repo.Get(id);

            if (server == null)
                return NotFound();

            return new ObjectResult(server);
        }

        // POST: api/Simulation
        [HttpPost]
        public IActionResult Post([FromBody]Simulation server)
        {
            if (server == null)
                return BadRequest();

            repo.Add(server);

            return CreatedAtRoute("Get", new { id = server.Id }, server);
        }

        // PUT: api/Simulation/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Simulation server)
        {
            if (server == null || server.Id != id)
                return BadRequest();
        
            var todo = repo.Get(id);
            if (todo == null)
                return NotFound();
        
            repo.Update(server);
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
