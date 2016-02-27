using System.Data.Entity.Migrations;

namespace CR.Application.Persistence.EF.Repos.Migrations
{
    internal sealed class AuthConfiguration : DbMigrationsConfiguration<AuthContext>
    {
        public AuthConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "ConfirmRep.Models.AuthContext";
        }
    }
}
