using CR.Application.Persistence.EF.Models;
using CR.Application.Persistence.EF.Repos.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CR.Application.Persistence.EF.Repos
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public AuthContext() : base("AuthContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthContext, AuthConfiguration>());
        }
    }
}