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
       public DbSet<EventType> EventTypes { get; set; }
       public DbSet<EventOrganisation> EventLocations { get; set; }
       public DbSet<Event> Events { get; set; }
       public DbSet<Ticket> Ticket { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventType>(ConfigureEventType);
            modelBuilder.Entity<EventOrganisation>(ConfigureEventOrganisation);
            modelBuilder.Entity<Event>(ConfigureEvents);
            modelBuilder.Entity<Ticket>(ConfigureTickets);
        }

        private void ConfigureTickets(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket");
            builder.Property(n => n.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("Event_Ticket_Hilo");
            builder.Property(n => n.Price)
             .IsRequired();
            builder.Property(n=> n.AvailableSeats)
                .IsRequired();
            builder.Property(n => n.ReservedSeats)
                .IsRequired();
            builder.Property(n=> n.TotalSeats)
                .IsRequired();
            builder.HasOne(n => n.Event)
                .WithMany()
                .HasForeignKey(n => n.EventId);
        }

        private void ConfigureEvents(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");
            builder.Property(e => e.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("Events");
            builder.Property(e => e.Name)
                 .IsRequired()
                 .HasMaxLength(100);
            builder.Property(e => e.Description)
                .IsRequired();
            builder.Property(e => e.Date)
                .IsRequired();
            builder.Property(e => e.AddressLine1)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.AddressLine2)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.city)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.Zipcode)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasOne(e => e.EventType)
               .WithMany()
               .HasForeignKey(e => e.EventTypeId);
            builder.HasOne(e => e.EventOrganisation)
                .WithMany()
                .HasForeignKey(e => e.EventOrganisationId);
            builder.HasMany(e => e.Tickets)
                .WithOne();

        }

        private void ConfigureEventOrganisation(EntityTypeBuilder<EventOrganisation> builder)
        {
            builder.ToTable("EventOrganisation");
            builder.Property(o =>o.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("Event_Organisation_hilo");
            builder.Property(o =>o.Name)
                .IsRequired()
                .HasMaxLength(100);

        }

        private void ConfigureEventType(EntityTypeBuilder<EventType> builder)
        {
            builder.ToTable("EventType");
            builder.Property(t => t.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("Event_Type_hilo");
            builder.Property(t => t.Name)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}
