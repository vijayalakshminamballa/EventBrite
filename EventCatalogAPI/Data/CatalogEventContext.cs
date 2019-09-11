using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Data
{
    public class CatalogEventContext:DbContext
    {
       public CatalogEventContext(DbContextOptions options):base(options)
        { }
       public DbSet<EventType> CatalogTypes { get; set; }
       public DbSet<EventLocation> CatalogLocations { get; set; }
       public DbSet<Event> CatalogEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventType>(ConfigureCatalogType);
            modelBuilder.Entity<EventLocation>(ConfigureCatalogLocation);
            modelBuilder.Entity<Event>(ConfigureCatalogEvents);
        }

        private void ConfigureCatalogEvents(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");
            builder.Property(e => e.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("Events");
            builder.Property(e => e.Name)
                 .IsRequired()
                 .HasMaxLength(100);
            builder.Property(e => e.Price)
                .IsRequired();
            builder.Property(e => e.availableSeats)
                .IsRequired();
            builder.Property(e => e.reservedSeats)
                .IsRequired();
            builder.Property(e => e.totalSeats)
                .IsRequired();
            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000);
            builder.Property(e => e.Date)
                .IsRequired();
            builder.HasOne(e => e.EventType)
                .WithMany()
                .HasForeignKey(e => e.EventTypeId);
            builder.HasOne(e => e.EventLocation)
                .WithMany()
                .HasForeignKey(e => e.EventLocationId);

        }

        private void ConfigureCatalogLocation(EntityTypeBuilder<EventLocation> builder)
        {
            builder.ToTable("CatalogLocation");
            builder.Property(l =>l.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("Event_Location_hilo");
            builder.Property(l =>l.Name)
                .IsRequired()
                .HasMaxLength(100);
        }

        private void ConfigureCatalogType(EntityTypeBuilder<EventType> builder)
        {
            builder.ToTable("CatalogTypes");
            builder.Property(t => t.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("Event_Type_hilo");
            builder.Property(t => t.Name)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}
