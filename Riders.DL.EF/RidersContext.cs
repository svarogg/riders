using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riders.Common.Model;

namespace Riders.DL.EF
{
    internal class RidersContext : DbContext
    {
        public RidersContext() : base("RidersContext")
        {
        }

        public DbSet<Bet> Bets { get; set; }
        public DbSet<Horse> Horses { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Rider> Riders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Rider>()
                   .HasRequired(rider => rider.Horse)
                   .WithMany()
                   .WillCascadeOnDelete(false);

            modelBuilder.Entity<Race>()
                .HasRequired(race => race.Rider1)
                .WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Race>()
                .HasRequired(race => race.Rider2)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bet>()
                .HasRequired(bet => bet.Rider)
                .WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Bet>()
                .HasRequired(bet => bet.Race)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
