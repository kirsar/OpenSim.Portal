using WebApi.Hal;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    public class UseResource : Representation
    {
        public long Id { get; set;}
        public string Name { get; set; }
        public string Description { get; set; }

        #region HAL

        public override string Rel
        {
            get => LinkTemplates.Users.User.Rel;
            set { }
        }

        public override string Href
        {
            get => LinkTemplates.Users.User.CreateLink(new { id = Id }).Href;
            set { }
        }

        #endregion
    }

    public class UserDetails
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public static class UserConvertions
    {
        public static UserDetails ToUserDetails(this User user) => new UserDetails
        {
            Id = user.Id,
            Name = user.Name,
            Description = user.Description
        };
    }
}
