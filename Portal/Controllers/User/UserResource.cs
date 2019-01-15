using WebApi.Hal;

namespace OpenSim.Portal.Controllers.User
{
    public sealed class UserInfoResource : Representation
    {
        private readonly Model.User.User user;

        public UserInfoResource(Model.User.User user)
        {
            this.user = user;
        }

        public UserInfoResource(Model.User.User user, string relationName) : this(user)
        {
            Rel = relationName;
        }

        public long Id => user.Id;
        public string Name => user.UserName;
        public string Description => user.Description;

        #region HAL

        public override string Rel { get; set; } = LinkTemplates.Users.User.Rel;

        public override string Href
        {
            get => LinkTemplates.Users.User.CreateLink(new { id = Id }).Href;
            set { }
        }

        #endregion
    }
}
