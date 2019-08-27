using LeagueViewer.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeagueViewer.DataAccess
{
    public class LeagueViewerContext : DbContext
    {
        public LeagueViewerContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<League> Leagues { get; set; }

        public virtual DbSet<Team> Teams { get; set; }

        public virtual DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
