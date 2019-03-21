using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OpenSim.Portal.Model
{
    public static class UserSeed
    {
        public const string User1 = "user";
        private const string User1Password = "User123$";
        private const string User1Description = "Regular user";

        public const string User2 = "UmbrellaCorp";
        private const string User2Password = "Umbrella123$";
        private const string User2Description = "Huge vendor of maritime simulators";

        private const string UserRole = "user";

        public static async void Seed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var provider = scope.ServiceProvider;

                // TODO dangerous for prod, use dedicated utility for migration
                provider.GetRequiredService<UserDbContext>().Database.Migrate();

                var userManager = provider.GetService<UserManager<User>>();
                var roleManager = provider.GetService<RoleManager<IdentityRole<long>>>();

                if (userManager.Users.Any())
                    return;

                await roleManager.CreateAsync(new IdentityRole<long>(UserRole));

                var user1 = new User(User1, User1Description);
                ThrowIfError(await userManager.CreateAsync(user1, User1Password));
                ThrowIfError(await userManager.AddToRoleAsync(user1, UserRole));

                var user2 = new User(User2, User2Description);
                ThrowIfError(await userManager.CreateAsync(user2, User2Password));
                ThrowIfError(await userManager.AddToRoleAsync(user2, UserRole));
            }
        }

        private static void ThrowIfError(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
                throw new InvalidOperationException(identityResult.Errors
                    .Aggregate(string.Empty, (r, e) => r += $"{e.Description}. "));
        }
    }
}