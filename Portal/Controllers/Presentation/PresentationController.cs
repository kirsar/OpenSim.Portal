using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenSim.Portal.Model;

namespace OpenSim.Portal.Controllers.Presentation
{
    [ApiVersion("1.0")]
    [Produces("application/hal+json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PresentationsController : Controller
    {
        private readonly IPresentationRepository repo;
        private readonly UserManager<Model.User> userManager;
        private readonly IEmbeddedRelationsSchema embeddedRelationsSchema;

        public PresentationsController(
            IPresentationRepository repo,
            UserManager<Model.User> userManager, 
            IEmbeddedRelationsSchema embeddedRelationsSchema)
        {
            this.repo = repo;
            this.userManager = userManager;
            this.embeddedRelationsSchema = embeddedRelationsSchema;
        }

        // GET: api/v1/presentations
        [HttpGet]
        public PresentationCollection Get() => new PresentationCollection(LinkTemplates.Presentations.Get.Rel,
                repo.GetAll().Select(presentation => new PresentationResource(presentation)))
            .EmbedRelations(Request, embeddedRelationsSchema, userManager);

        // GET: api/v1/presentations/5
        [HttpGet("{id}")]
        public ActionResult<PresentationResource> Get(int id)
        {
            var presentation = repo.Get(id);

            if (presentation == null)
                return NotFound();

            return new PresentationResource(presentation).EmbedRelations(Request, embeddedRelationsSchema, userManager);
        }

        // POST: api/v1/presentations/5
        [HttpPost]
        public IActionResult Post([FromBody]PresentationResource presentation)
        {
            if (presentation == null)
                return BadRequest();

            repo.Add(new Model.Presentation
            {
                Name = presentation.Name,
                // TODO
            });

            return CreatedAtRoute("Get", new { id = presentation.Id }, presentation);
        }
    }
}
