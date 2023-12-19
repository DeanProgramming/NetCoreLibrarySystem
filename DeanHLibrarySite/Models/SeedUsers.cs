using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DeanHLibrarySite.Models; // Make sure to update this namespace based on your project structure
using DeanHLibrarySite.Data;
using DeanHLibrarySite.Pages;
using System.Numerics;

namespace DeanHLibrarySite.Models
{
    public static class SeedUsers
    {
        public static string UserOne = "4e563731-e00e-4613-8d48-43292ae95427";
        public static string UserTwo = "9fdedffd-7c19-43db-9ffc-85926be90d6d";
        public static string UserThree = "a5f3bdc9-737b-4d6b-8775-480888ab54e9";
        public static string UserFour = "c5a14c1e-053c-4527-be74-ad9ad48c35c2";
        public static string Admin = "2ae3d199-3f90-45f4-9353-33c98bae56b8";


        public static async Task InitializeAsync(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager)
        {
            // Ensure the context is created
            using (var context = new DeanHLibrarySiteContext(
                serviceProvider.GetRequiredService<DbContextOptions<DeanHLibrarySiteContext>>()))
            {
                // Check if the context or BookReservations is null
                if (context == null || context.Users == null)
                {
                    throw new ArgumentNullException("Null DeanHLibrarySiteContext or Users");
                }

                // Check if there are any users in the database
                if (context.Users.Any())
                {
                    return; // DB has been seeded
                }

                var userId = Admin;
                var existingUser = await userManager.FindByIdAsync(userId);


                // If the user already exists, do nothing
                if (existingUser != null)
                {
                    return;
                }

                // Create an admin user
                var user = CreateUser();

                user.Id = userId;
                user.Name = "Admin";
                user.DOB = DateTime.Now;
                user.IsAdmin = true;

                // Set user properties
                await userStore.SetUserNameAsync(user, "Admin@Admin.com", CancellationToken.None);
                await GetEmailStore(userManager, userStore).SetEmailAsync(user, "Admin@Admin.com", CancellationToken.None);

                user.EmailConfirmed = true;

                // Create the user with the specified password
                var result = await userManager.CreateAsync(user, "Admin!1");
                 

                // Create an User One user
                user = CreateUser();

                user.Id = UserOne;
                user.Name = "UserOne";
                user.DOB = DateTime.Now;
                user.IsAdmin = false;

                // Set user properties
                await userStore.SetUserNameAsync(user, "UserOne@UserOne.com", CancellationToken.None);
                await GetEmailStore(userManager, userStore).SetEmailAsync(user, "UserOne@UserOne.com", CancellationToken.None);
                user.EmailConfirmed = true;

                // Create the user with the specified password
                result = await userManager.CreateAsync(user, "UserOne!1");

                // Create an User Two user
                user = CreateUser();

                user.Id = UserTwo;
                user.Name = "UserTwo";
                user.DOB = DateTime.Now;
                user.IsAdmin = false;

                // Set user properties
                await userStore.SetUserNameAsync(user, "UserTwo@UserTwo.com", CancellationToken.None);
                await GetEmailStore(userManager, userStore).SetEmailAsync(user, "UserTwo@UserTwo.com", CancellationToken.None);
                user.EmailConfirmed = true;

                // Create the user with the specified password
                result = await userManager.CreateAsync(user, "UserTwo!1");

                // Create an User Three user
                user = CreateUser();

                user.Id = UserThree;
                user.Name = "UserThree";
                user.DOB = DateTime.Now;
                user.IsAdmin = false;

                // Set user properties
                await userStore.SetUserNameAsync(user, "UserThree@UserThree.com", CancellationToken.None);
                await GetEmailStore(userManager, userStore).SetEmailAsync(user, "UserThree@UserThree.com", CancellationToken.None);
                user.EmailConfirmed = true;

                // Create the user with the specified password
                result = await userManager.CreateAsync(user, "UserThree!1");

                // Create an User Four user
                user = CreateUser();

                user.Id = UserFour;
                user.Name = "UserFour";
                user.DOB = DateTime.Now;
                user.IsAdmin = false;

                // Set user properties
                await userStore.SetUserNameAsync(user, "UserFour@UserFour.com", CancellationToken.None);
                await GetEmailStore(userManager, userStore).SetEmailAsync(user, "UserFour@UserFour.com", CancellationToken.None);
                user.EmailConfirmed = true;

                // Create the user with the specified password
                result = await userManager.CreateAsync(user, "UserFour!1");

                // Add the user to the context and save changes
                //context.Users.Add(user);
                context.SaveChanges();
            }
        }

        private static ApplicationUser CreateUser()
        {
            try
            {
                // Create an instance of ApplicationUser
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                                                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                                                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private static IUserEmailStore<ApplicationUser> GetEmailStore(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore)
        {
            // Check if the user manager supports user email
            if (!userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }

            // Return the IUserEmailStore<ApplicationUser> interface
            return (IUserEmailStore<ApplicationUser>)userStore;
        }
    }
}
