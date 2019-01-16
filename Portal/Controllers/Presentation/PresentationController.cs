using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OpenSim.Portal.Model.Presentation;

namespace OpenSim.Portal.Controllers.Presentation
{
    [ApiVersion("1.0")]
    [Produces("application/hal+json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PresentationsController : Controller
    {
        private readonly IPresentationRepository repo;
        private readonly IEmbeddedRelationsSchema embeddedRelationsSchema;

        public PresentationsController(IPresentationRepository repo, IEmbeddedRelationsSchema embeddedRelationsSchema)
        {
            this.repo = repo;
            this.embeddedRelationsSchema = embeddedRelationsSchema;
        }

        // GET: api/v1/presentations
        [HttpGet]
        public PresentationCollection Get() => new PresentationCollection(LinkTemplates.Presentations.GetPresentations.Rel,
                repo.GetAll().Select(presentation => new PresentationResource(presentation)))
            .EmbedRelations(Request, embeddedRelationsSchema);

        // GET: api/v1/presentations/5
        [HttpGet("{id}")]
        public ActionResult<PresentationResource> Get(int id)
        {
            var presentation = repo.Get(id);

            if (presentation == null)
                return NotFound();

            return new PresentationResource(presentation).EmbedRelations(Request, embeddedRelationsSchema);
        }

        // POST: api/v1/presentations/5
        [HttpPost]
        public IActionResult Post([FromBody]PresentationResource presentation)
        {
            if (presentation == null)
                return BadRequest();

            repo.Add(new Model.Presentation.Presentation
            {
                Name = presentation.Name,
                // TODO
            });

            return CreatedAtRoute("Get", new { id = presentation.Id }, presentation);
        }
    }
}
