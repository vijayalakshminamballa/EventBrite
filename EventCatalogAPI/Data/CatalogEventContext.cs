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
       public DbSet<EventType> EventType { get; set; }
       public DbSet<EventCategory>EventCategories { get; set; }
       public DbSet<EventItem> EventItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventType>(ConfigureEventType);
            modelBuilder.Entity<EventCategory>(ConfigureEventCategory);
            modelBuilder.Entity<EventItem>(ConfigureEventItem);
        }



        private void ConfigureEventItem(EntityTypeBuilder<EventItem> builder)
        {
            builder.ToTable("Events");
            builder.Property(e => e.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("EventItems");
            builder.Property(e => e.Name)
                 .IsRequired()
                 .HasMaxLength(100);
            builder.Property(e => e.Description)
                .IsRequired();
            builder.Property(e => e.Date)
                .IsRequired();
            builder.Property(e => e.Price)
                .IsRequired();
            builder.Property(e => e.Organizer)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.AddressLine)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.State)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasOne(e => e.EventType)
               .WithMany()
               .HasForeignKey(e => e.EventTypeId);
            builder.HasOne(e => e.EventCategory)
                .WithMany()
                .HasForeignKey(e => e.EventCategoryId);

        }

        private void ConfigureEventCategory(EntityTypeBuilder<EventCategory> builder)
        {
            builder.ToTable("EventCategory");
            builder.Property(c =>c.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("Event_Category_hilo");
            builder.Property(c =>c.Category)
                .IsRequired()
                .HasMaxLength(100);

        }

        private void ConfigureEventType(EntityTypeBuilder<EventType> builder)
        {
            builder.ToTable("EventType");
            builder.Property(t => t.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("Event_Type_hilo");
            builder.Property(t => t.Type)
                 .IsRequired()
                 .HasMaxLength(100);
        }
    }
}
