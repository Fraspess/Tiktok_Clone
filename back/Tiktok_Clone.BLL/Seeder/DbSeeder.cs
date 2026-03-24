using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
using Tiktok_Clone.BLL.Dtos.Role;
using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.BLL.Services.ImageService;
using Tiktok_Clone.DAL;
using Tiktok_Clone.DAL.Entities.Identity;

namespace Tiktok_Clone.BLL.Seeder
{
    public static class DbSeeder
    {

        // All json seed files should be located in Tiktok_Clone/Helpers and have Copy To Output Directory
        public static async Task SeedDataAsync(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var imageService = scope.ServiceProvider.GetRequiredService<IImageService>();

            context.Database.Migrate();

            await SeedRolesAsync(roleManager, environment);
            await SeedUsersAsync(userManager, imageService, environment);
        }

        public static async Task SeedRolesAsync(RoleManager<RoleEntity> roleManager, IWebHostEnvironment environment)
        {
            if (!roleManager.Roles.Any())
            {
                var json = File.ReadAllText(Path.Combine(environment.ContentRootPath, "Helpers", "Roles.json"));
                var roles = JsonConvert.DeserializeObject<List<SeedRoleDTO>>(json);

                if (roles == null)
                {
                    Log.Error("Failed to get roles from json file to seed databse");
                    return;
                }
                foreach (var role in roles)
                {
                    var newRole = new RoleEntity()
                    {
                        Name = role.Name
                    };

                    var result = await roleManager.CreateAsync(newRole);

                    if (result.Succeeded)
                    {
                        Log.Information("Role {RoleName} seeded successfully", role.Name);
                    }
                    else
                    {
                        Log.Error("Failed to seed role {RoleName}. Errors : {Errors}",
                            role.Name,
                            string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }
            }
            else
            {
                Log.Information("Roles already exists in database, skipping seeding");
            }
        }

        public static async Task SeedUsersAsync(UserManager<UserEntity> userManager, IImageService imageService, IWebHostEnvironment environment)
        {

            if (!userManager.Users.Any())
            {
                var json = File.ReadAllText(Path.Combine(environment.ContentRootPath, "Helpers", "Users.json"));
                var users = JsonConvert.DeserializeObject<List<SeedUserDTO>>(json);

                if (users == null)
                {
                    Log.Error("Failed to get users from json file to seed databse");
                    return;
                }

                foreach (var user in users)
                {
                    var newUser = new UserEntity()
                    {
                        UserName = user.Username,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Avatar = await imageService.SaveImageAsync(user.Image!)
                    };

                    var result = await userManager.CreateAsync(newUser, user.Password!);
                    if (result.Succeeded)
                    {
                        var resultR = await userManager.AddToRolesAsync(newUser, user.Roles!);
                        if (resultR.Succeeded)
                        {
                            Log.Information("User {UserName} seeded successfully", user.Username);
                        }
                        else
                        {
                            Log.Error("Failed to assign roles. Error: {Errors}",
                                string.Join(", ", resultR.Errors.Select(e => e.Description)));
                        }
                    }
                    else
                    {
                        Log.Error("Failed to seed user {UserName}. Errors : {Errors}",
                            user.Username,
                            string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }
            }
            else
            {
                Log.Information("Users already exists in database, skipping seeding");
            }
        }
    }
}
