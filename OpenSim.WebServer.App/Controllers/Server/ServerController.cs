using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OpenSim.WebServer.App.Controllers.Server
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ServerController : Controller
    {
        private IServerRepository repo;

        public ServerController(IServerRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/Server
        [HttpGet]
        public IEnumerable<Server> Get() => repo.GetAll();

        // GET: api/Server/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var server = repo.Find(id);

            if (server == null)
                return NotFound();

            return new ObjectResult(server);
        }
        
        // POST: api/Server
        [HttpPost]
        public IActionResult Post([FromBody]Server server)
        {
            if (server == null)
                return BadRequest();

            repo.Add(server);

            return CreatedAtRoute("Get", new { id = server.Id }, server);
        }
        
        // PUT: api/Server/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Server server)
        {
            if (server == null || server.Id != id)
                return BadRequest();
        
            var todo = repo.Find(id);
            if (todo == null)
                return NotFound();
        
            repo.Update(server);
            return new NoContentResult();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sever = repo.Find(id);
            if (repo == null)
                return NotFound();
            
            repo.Remove(id);
            return new NoContentResult();
        }
    }
}
