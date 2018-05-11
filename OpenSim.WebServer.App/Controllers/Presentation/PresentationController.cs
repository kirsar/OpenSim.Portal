using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PresentationController : Controller
    {
        private readonly IPresentationRepository repo;

        public PresentationController(IPresentationRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/v1/presentations
        [HttpGet]
        public IEnumerable<PresentationResource> Get() => repo.GetAll().Select(p => new PresentationResource(p));

        // GET: api/v1/presentations/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var presentation = repo.Get(id);

            if (presentation == null)
                return NotFound();

            return new ObjectResult(new PresentationResource(presentation));
        }

        // POST: api/v1/presentations/5
        [HttpPost]
        public IActionResult Post([FromBody]PresentationResource presentation)
        {
            if (presentation == null)
                return BadRequest();

            repo.Add(new Presentation
            {
                Name = presentation.Name,
                // TODO
            });

            return CreatedAtRoute("Get", new { id = presentation.Id }, presentation);
        }

        // PUT: api/v1/presentations/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PresentationResource presentation)
        {
            if (presentation == null || presentation.Id != id)
                return BadRequest();
        
            var todo = repo.Get(id);
            if (todo == null)
                return NotFound();
        
            repo.Update(new Presentation
            {
                Id = presentation.Id,
                Name = presentation.Name,
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
