using ChfApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace ChfApi.Data
{
    public class Seed
    {
        public static async Task SeedAsync(DataContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ILoggerFactory loggerFactory)
        {
            try
            {
                if (await context.Categories.AnyAsync()) return;
                var categories = new List<Category>
                {
                    new Category { CategoryName = "Pathology" },
                    new Category { CategoryName = "ECG" },
                };
                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();


                if (await context.Employees.AnyAsync()) return;
                var employeeData = File.ReadAllText("./Data/Employee.json");
                var employees = JsonSerializer.Deserialize<List<Employee>>(employeeData);
                if (employees == null) return;
                context.Employees.AddRange(employees);
                await context.SaveChangesAsync();

                if (await context.Doctors.AnyAsync()) return;
                var doctorData = File.ReadAllText("./Data/Doctor.json");
                var doctors = JsonSerializer.Deserialize<List<Doctor>>(doctorData);
                if (doctors == null) return;
                context.Doctors.AddRange(doctors);
                await context.SaveChangesAsync();

                if (await context.Tests.AnyAsync()) return;
                var productData = File.ReadAllText("./Data/Test.json");
                var products = JsonSerializer.Deserialize<List<Test>>(productData);
                if (products == null) return;

                context.Tests.AddRange(products);
                await context.SaveChangesAsync();


                if (await context.ExpenseCategories.AnyAsync()) return;

                var expCategoryDataFile = File.ReadAllText("./Data/ExpenseCategory.json");
                var expCategories = JsonSerializer.Deserialize<List<ExpenseCategory>>(expCategoryDataFile);
                if (expCategories == null) return;

                context.ExpenseCategories.AddRange(expCategories);
                await context.SaveChangesAsync();

                if (await userManager.Users.AnyAsync()) return;
                var usersData = File.ReadAllText("./Data/User.json");
                var users = JsonSerializer.Deserialize<List<AppUser>>(usersData);
                if (users == null) return;

                var roles = new List<AppRole>
                    {
                        new AppRole{Name = "Administrator"},
                        new AppRole{Name = "Staff"},
                    };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }

                foreach (var user in users)
                {
                    user.UserName = user.UserName.ToLower();
                    await userManager.CreateAsync(user, "Pa$$word123");
                    await userManager.AddToRoleAsync(user, "Staff");
                }
                var admin = new AppUser
                {
                    UserName = "admin",
                    KnownAs = "Admin",
                };

                await userManager.CreateAsync(admin, "Pa$$w0rd123");
                await userManager.AddToRolesAsync(admin, new[] { "Administrator", "Staff" });
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Seed>();
                logger.LogError(ex.Message);
            }


        }
    }
}
