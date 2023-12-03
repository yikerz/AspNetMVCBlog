/* 159. Create DbContext inherited from IdentityDbContext */
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        /* 160. Create constructor */
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }
        /* 161. Override OnModelCreating */
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* 162. Create list of IdentityRole */
            var adminRoleId = "4dc7d806-eab6-4e34-8d25-d0a3f51c611b";
            var superAdminRoleId = "4c148ae8-5b69-4ccb-b97e-56f2a5287247";
            var userRoleId = "28b92e50-5e55-457d-90b5-03dced300e00";
            var roles = new List<IdentityRole>();
            var userRole = new IdentityRole
            {
                Name = "User",
                NormalizedName = "User",
                Id = userRoleId,
                ConcurrencyStamp = userRoleId
            };
            var adminRole = new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "Admin",
                Id = adminRoleId,
                ConcurrencyStamp = adminRoleId
            };
            var superAdminRole = new IdentityRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SuperAdmin",
                Id = superAdminRoleId,
                ConcurrencyStamp = superAdminRoleId
            };
            roles.Add(userRole);
            roles.Add(adminRole);
            roles.Add(superAdminRole);            
            /* 163. Build entity with initial data */
            builder.Entity<IdentityRole>().HasData(roles);
            /* 164. Create IdentityUser for super admin */
            var superAdminId = "d7dabf9b-f553-4063-8f44-7613cfcf36f4";
            var superAdminUser = new IdentityUser
            {
                UserName = "yikerz.testing@gmail.com",
                Email = "yikerz.testing@gmail.com",
                NormalizedEmail = "yikerz.testing@gmail.com".ToUpper(),
                NormalizedUserName = "yikerz.testing@gmail.com".ToUpper(),
                Id = superAdminId,
            };
            /* 165. Create hashed password for super admin */
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "AdminPass@123");
            /* 166. Build entity with initial data */
            builder.Entity<IdentityUser>().HasData(superAdminUser);
            /* 167. Create superAdminRoles and add all roles */
            var superAdminRoles = new List<IdentityUserRole<string>>();
            var superAdminRole1 = new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = superAdminId,
            };
            var superAdminRole2 = new IdentityUserRole<string>
            {
                RoleId = superAdminRoleId,
                UserId = superAdminId,
            };
            var superAdminRole3 = new IdentityUserRole<string>
            {
                RoleId = userRoleId,
                UserId = superAdminId,
            };
            superAdminRoles.Add(superAdminRole1);
            superAdminRoles.Add(superAdminRole2);
            superAdminRoles.Add(superAdminRole3);
            /* 168. Build entity with initial data */
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
