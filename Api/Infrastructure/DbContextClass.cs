using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infrastructure
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanSubscription> PlanSubscriptions { get; set; }
        public DbSet<Professional> Professionals { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Leader> Leaders { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CheckRecord> CheckRecords { get; set; }
        public DbSet<Recurrence> Recurrences { get; set; }
        public DbSet<GpsTracking> GpsTrackings { get; set; }

        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Companies");
                entity.HasKey(c => c.Id);
                entity.HasMany(c => c.Users)
                      .WithOne()
                      .HasForeignKey(u => u.CompanyId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("Plans");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.Price).HasPrecision(18, 2);
                entity.Property(p => p.Status).HasConversion<int>();
                entity.Property(p => p.Features).HasColumnType("text[]");
                entity.HasMany(p => p.Subscriptions)
                      .WithOne(s => s.Plan)
                      .HasForeignKey(s => s.PlanId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PlanSubscription>(entity =>
            {
                entity.ToTable("PlanSubscriptions");
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Status).HasConversion<int>();
                entity.HasOne(s => s.Company)
                      .WithMany()
                      .HasForeignKey(s => s.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Professional>(entity =>
            {
                entity.ToTable("Professionals");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.Email).IsRequired();
                entity.Property(p => p.Status).HasConversion<string>();
                entity.Property(p => p.CreatedDate).HasDefaultValueSql("now()");
                entity.Property(p => p.UpdatedDate).HasDefaultValueSql("now()");
                entity.HasOne(p => p.Company)
                      .WithMany()
                      .HasForeignKey(p => p.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("Teams");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired();
                entity.Property(t => t.Status).HasConversion<string>();
                entity.Property(t => t.Region);
                entity.Property(t => t.Description);
                entity.Property(t => t.Rating);
                entity.Property(t => t.CompletedServices);
                entity.Property(t => t.CreatedDate).HasDefaultValueSql("now()");
                entity.Property(t => t.UpdatedDate).HasDefaultValueSql("now()");
                entity.HasOne(t => t.Company)
                      .WithMany()
                      .HasForeignKey(t => t.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(t => t.Leader)
                      .WithMany()
                      .HasForeignKey(t => t.LeaderId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Leader>(entity =>
            {
                entity.ToTable("Leaders");
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Name).IsRequired();
                entity.Property(l => l.Email).IsRequired();
                entity.Property(l => l.Phone);
                entity.Property(l => l.Status).HasConversion<string>();
                entity.Property(l => l.CreatedDate).HasDefaultValueSql("now()");
                entity.Property(l => l.UpdatedDate).HasDefaultValueSql("now()");
                entity.HasOne(l => l.User)
                      .WithMany()
                      .HasForeignKey(l => l.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointments");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Title).IsRequired();
                entity.Property(a => a.Address);
                entity.Property(a => a.Start).IsRequired();
                entity.Property(a => a.End).IsRequired();
                entity.Property(a => a.Status).HasConversion<string>().IsRequired();
                entity.Property(a => a.Type).HasConversion<string>().IsRequired();
                entity.Property(a => a.Notes);
                entity.Property(a => a.CreatedDate).HasDefaultValueSql("now()");
                entity.Property(a => a.UpdatedDate).HasDefaultValueSql("now()");
                entity.HasOne(a => a.Company)
                      .WithMany()
                      .HasForeignKey(a => a.CompanyId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(a => a.Customer)
                      .WithMany()
                      .HasForeignKey(a => a.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(a => a.Team)
                      .WithMany()
                      .HasForeignKey(a => a.TeamId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(a => a.Professional)
                      .WithMany()
                      .HasForeignKey(a => a.ProfessionalId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired();
                entity.Property(c => c.Document).IsRequired();
                entity.Property(c => c.Email);
                entity.Property(c => c.Phone);
                entity.Property(c => c.Address);
                entity.Property(c => c.City);
                entity.Property(c => c.State);
                entity.Property(c => c.Observations);
                entity.Property(c => c.Status).HasConversion<string>();
                entity.Property(c => c.CreatedDate).HasDefaultValueSql("now()");
                entity.Property(c => c.UpdatedDate).HasDefaultValueSql("now()");
                entity.HasOne(c => c.Company)
                      .WithMany()
                      .HasForeignKey(c => c.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CheckRecord>(entity =>
            {
                entity.ToTable("CheckRecords");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.ProfessionalId).IsRequired();
                entity.Property(c => c.CompanyId).IsRequired();
                entity.Property(c => c.CustomerId).IsRequired();
                entity.Property(c => c.AppointmentId).IsRequired();
                entity.Property(c => c.Address).IsRequired();
                entity.Property(c => c.ServiceType).IsRequired();
                entity.Property(c => c.Status).HasConversion<int>();
                entity.Property(c => c.Notes);
                entity.Property(c => c.ProfessionalName);
                entity.Property(c => c.CustomerName);
                entity.Property(c => c.TeamId);
                entity.Property(c => c.TeamName);
                entity.Property(c => c.CheckInTime);
                entity.Property(c => c.CheckOutTime);
                entity.Property(c => c.CreatedDate).HasDefaultValueSql("now()");
                entity.Property(c => c.UpdatedDate).HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<Recurrence>(entity =>
            {
                entity.ToTable("Recurrences");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.CompanyId).IsRequired();
                entity.Property(r => r.CustomerId);
                entity.Property(r => r.TeamId);
                entity.Property(r => r.Title).IsRequired().HasMaxLength(200);
                entity.Property(r => r.Description);
                entity.Property(r => r.Address);
                entity.Property(r => r.Frequency).HasConversion<int>().IsRequired();
                entity.Property(r => r.Day);
                entity.Property(r => r.Time).IsRequired();
                entity.Property(r => r.Duration).IsRequired();
                entity.Property(r => r.Status).HasConversion<int>().IsRequired();
                entity.Property(r => r.Type).HasConversion<int>().IsRequired();
                entity.Property(r => r.StartDate).IsRequired();
                entity.Property(r => r.EndDate);
                entity.Property(r => r.Notes);
                entity.Property(r => r.LastExecution);
                entity.Property(r => r.NextExecution);
                entity.Property(r => r.CreatedDate).HasDefaultValueSql("now()");
                entity.Property(r => r.UpdatedDate).HasDefaultValueSql("now()");
                entity.HasOne(r => r.Company).WithMany().HasForeignKey(r => r.CompanyId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(r => r.Customer).WithMany().HasForeignKey(r => r.CustomerId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(r => r.Team).WithMany().HasForeignKey(r => r.TeamId).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<GpsTracking>(entity =>
            {
                entity.ToTable("GpsTrackings");
                entity.HasKey(g => g.Id);

                entity.Property(g => g.ProfessionalId).IsRequired();
                entity.Property(g => g.ProfessionalName);
                entity.Property(g => g.CompanyId).IsRequired();
                entity.Property(g => g.CompanyName);
                entity.Property(g => g.Vehicle).IsRequired();

                // Mapeia Location como owned type
                entity.OwnsOne(g => g.Location, loc =>
                {
                    loc.Property(l => l.Latitude)
                        .HasColumnName("Latitude")
                        .IsRequired();
                    loc.Property(l => l.Longitude)
                        .HasColumnName("Longitude")
                        .IsRequired();
                    loc.Property(l => l.Address)
                        .HasColumnName("Address");
                    loc.Property(l => l.Accuracy)
                        .HasColumnName("Accuracy")
                        .IsRequired();
                });

                entity.Property(g => g.Speed).IsRequired();
                entity.Property(g => g.Status)
                      .HasConversion<int>()
                      .IsRequired();
                entity.Property(g => g.Battery).IsRequired();
                entity.Property(g => g.Notes);
                entity.Property(g => g.Timestamp).IsRequired();
                entity.Property(g => g.CreatedDate).HasDefaultValueSql("now()");
                entity.Property(g => g.UpdatedDate).HasDefaultValueSql("now()");
            });

            // Review mapping:
            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Reviews");
                entity.HasKey(r => r.Id);

                entity.Property(r => r.CustomerId).IsRequired();
                entity.Property(r => r.CustomerName);
                entity.Property(r => r.ProfessionalId);
                entity.Property(r => r.ProfessionalName);
                entity.Property(r => r.TeamId);
                entity.Property(r => r.TeamName);
                entity.Property(r => r.CompanyId).IsRequired();
                entity.Property(r => r.CompanyName);
                entity.Property(r => r.AppointmentId).IsRequired();
                entity.Property(r => r.Rating).IsRequired();
                entity.Property(r => r.Comment);
                entity.Property(r => r.Date).IsRequired();
                entity.Property(r => r.ServiceType).IsRequired();
                entity.Property(r => r.Status)
                      .HasConversion<int>()
                      .IsRequired();
                entity.Property(r => r.Response);
                entity.Property(r => r.ResponseDate);
                entity.Property(r => r.CreatedDate).HasDefaultValueSql("now()");
                entity.Property(r => r.UpdatedDate).HasDefaultValueSql("now()");
            });

            base.OnModelCreating(modelBuilder);


        }



        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseModel &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseModel)entry.Entity;
                entity.UpdatedDate = DateTime.UtcNow;
                if (entry.State == EntityState.Added)
                    entity.CreatedDate = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }
    }
}
