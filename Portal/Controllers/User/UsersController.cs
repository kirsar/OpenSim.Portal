using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OpenSim.Portal.Controllers.User
{
    [ApiVersion("1.0")]
    [Produces("application/hal+json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserManager<Model.User> userManager;
       
        public UsersController(UserManager<Model.User> userManager)
        {
            this.userManager = userManager;
        }

        // GET: api/v1/Users/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<UserInfoResource> Get(long id)
        {
            var user = userManager.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            return new UserInfoResource(user);
        }
    }
}
