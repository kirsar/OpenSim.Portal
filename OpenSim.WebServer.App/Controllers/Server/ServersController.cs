﻿using Microsoft.AspNetCore.Mvc;

namespace OpenSim.WebServer.App.Controllers.Server
{
    [ApiVersion("1.0")]
    [Produces("application/hal+json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ServersController : Controller
    {
        private readonly IServerRepository repo;

        public ServersController(IServerRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/v1/Servers
        [HttpGet]
        public ServerCollection Get() => repo.GetAll();

        // GET: api/v1/Servers/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var server = repo.Get(id);

            if (server == null)
                return NotFound();

            return new ObjectResult(server);
        }
        
        // POST: api/v1/Servers
        [HttpPost]
        public IActionResult Post([FromBody]Server server)
        {
            if (server == null)
                return BadRequest();

            repo.Add(server);

            return CreatedAtRoute("Get", new { id = server.Id }, server);
        }
        
        // PUT: api/v1/Servers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Server server)
        {
            if (server == null || server.Id != id)
                return BadRequest();
        
            var todo = repo.Get(id);
            if (todo == null)
                return NotFound();
        
            repo.Update(server);
            return new NoContentResult();
        }
        
        // DELETE: api/v1/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sever = repo.Get(id);
            if (repo == null)
                return NotFound();
            
            repo.Remove(id);
            return new NoContentResult();
        }
    }
}