using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infrastructure
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> contextOptions) : base(contextOptions) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanSubscription> PlanSubscriptions { get; set; }
        public DbSet<Professional> Professionals { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Leader> Leaders { get; set; }

        // ✅ Adicionado
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);
            });

            // Companies
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Companies");
                entity.HasKey(c => c.Id);

                entity.HasMany(c => c.Users)
                      .WithOne()
                      .HasForeignKey(u => u.CompanyId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Plans
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

            // PlanSubscriptions
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

            // Professionals
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

            // Teams
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

            // Leaders
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

            // ✅ Appointment
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
                {
                    entity.CreatedDate = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }
    }
}
