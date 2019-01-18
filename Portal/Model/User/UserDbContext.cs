using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OpenSim.Portal.Model
{
    public class UserDbContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
    }
}