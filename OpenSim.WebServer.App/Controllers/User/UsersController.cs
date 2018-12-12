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
        private readonly SignInManager<User> signInManager;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async void Login(string name, string password)
        {
            var user = await userManager.FindByNameAsync(name);
            if (user != null)
            {
                await signInManager.SignOutAsync();
                await signInManager.PasswordSignInAsync(user, password, false, false);
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Logout(string redirectUrl)
        //{
        //    await signInManager.SignOutAsync();
        //    return Redirect(redirectUrl ?? "/");
        //}

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
