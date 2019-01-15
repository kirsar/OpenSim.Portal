using System;
using Microsoft.AspNetCore.Identity;

namespace OpenSim.Portal.Model.User
{
    public sealed class User : IdentityUser<long>
    {
        public User(string userName, string description) : base(userName)
        {
            Description = description;
            SecurityStamp = Guid.NewGuid().ToString();
        }

        public string Description { get; set; }
    }
}