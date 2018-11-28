using Microsoft.AspNetCore.Mvc;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/hal+json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository repo;

        public UsersController(IUserRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/v1/Users/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<UserInfoResource> Get(int id)
        {
            var user = repo.Get(id);

            if (user == null)
                return NotFound();

            return new UserInfoResource(user);
        }
    }
}
