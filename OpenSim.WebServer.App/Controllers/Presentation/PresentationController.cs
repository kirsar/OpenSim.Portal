using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OpenSim.WebServer.App.Controllers.Presentation
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PresentationController : Controller
    {
        private readonly IPresentationRepository repo;

        public PresentationController(IPresentationRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/Simulation
        [HttpGet]
        public IEnumerable<Presentation> Get() => repo.GetAll();

        // GET: api/Simulation/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var presentation = repo.Get(id);

            if (presentation == null)
                return NotFound();

            return new ObjectResult(presentation);
        }

        // POST: api/Simulation
        [HttpPost]
        public IActionResult Post([FromBody]Presentation presentation)
        {
            if (presentation == null)
                return BadRequest();

            repo.Add(presentation);

            return CreatedAtRoute("Get", new { id = presentation.Id }, presentation);
        }

        // PUT: api/Simulation/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Presentation presentation)
        {
            if (presentation == null || presentation.Id != id)
                return BadRequest();
        
            var todo = repo.Get(id);
            if (todo == null)
                return NotFound();
        
            repo.Update(presentation);
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
