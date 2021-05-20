using Microsoft.EntityFrameworkCore;

namespace aspnetcoreapp.Models
{
    // reference
    // dotnet tool install --global dotnet-ef
    // dotnet add package Microsoft.EntityFrameworkCore.Design
    // dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL


    // setup
    // dotnet ef migrations add InitialCreateToPostgreSQL // create a migration
    // dotnet ef database update                          // apply migration

    // clean
    // dotnet ef database update 0 // revert all migration 
    // dotnet ef migrations remove // remove all migration
    public class PostgreDbContext : DbContext
    {
        public PostgreDbContext(DbContextOptions<PostgreDbContext> options)
            : base(options)
        {
        }
        public DbSet<MessageModel> Messages { get; set; }

        // The following configures EF to create a postgres database on heroku.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql(PostgreDb.GetConnectionStringFromHerokuEnv());
    }
}
