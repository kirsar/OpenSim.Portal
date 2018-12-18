using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenSim.WebServer.App.Model;

namespace OpenSim.WebServer.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/hal+json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
       
        public UsersController(UserManager<User> userManager)
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
