using System;
using Microsoft.AspNetCore.Identity;

namespace OpenSim.WebServer.App.Model
{
    public sealed class User : IdentityUser<long>
    {
        public User(long id, string userName) : this(id, userName, null)
        {
        }

        public User(long id, string userName, string description) : base(userName)
        {
            Id = id;
            Description = description;
            SecurityStamp = Guid.NewGuid().ToString();
        }

        public string Description { get; set; }
    }
}