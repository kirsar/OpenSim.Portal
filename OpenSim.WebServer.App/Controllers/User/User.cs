namespace OpenSim.WebServer.App.Controllers.User
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class UserDetails
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public static class UserConvertions
    {
        public static UserDetails ToUserDetails(this User user) => new UserDetails
            {
                Name = user.Name,
                Description = user.Description
            };
    }
}
