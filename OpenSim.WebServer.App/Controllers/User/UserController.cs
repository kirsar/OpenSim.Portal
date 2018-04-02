using Microsoft.AspNetCore.Mvc;

namespace OpenSim.WebServer.App.Controllers.User
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository repo;

        public UserController(IUserRepository repo)
        {
            this.repo = repo;
        }

        // TODO now this will show sensitive data
        //// GET: api/User
        //[HttpGet]
        //public IEnumerable<User> Get() => repo.GetAll();

        //// GET: api/Server/5
        //[HttpGet("{id}", Name = "Get")]
        //public IActionResult Get(int id)
        //{
        //    var user = repo.Get(id);

        //    if (user == null)
        //        return NotFound();

        //    return new ObjectResult(user);
        //}

        // GET: api/User/5/details
        [HttpGet("{id}/details")]
        public IActionResult GetDetails(int id)
        {
            var userDetails = repo.GetDetails(id);

            if (userDetails == null)
                return NotFound();

            return new ObjectResult(userDetails);
        }

        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            if (user == null)
                return BadRequest();

            repo.Add(user);

            return CreatedAtRoute("Get", new { id = user.Id }, user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]User user)
        {
            if (user == null || user.Id != id)
                return BadRequest();
        
            var todo = repo.Get(id);
            if (todo == null)
                return NotFound();
        
            repo.Update(user);
            return new NoContentResult();
        }
        
        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var sever = repo.Get(id);
        //    if (repo == null)
        //        return NotFound();
            
        //    repo.Remove(id);
        //    return new NoContentResult();
        //}
    }
}
