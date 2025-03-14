using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ChildPlayCenter.Models;

public class PlayCenterContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<GameMachine> GameMachines { get; set; }
    public DbSet<MachineStatus> MachineStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=child_play_center;Username=postgres")
                      .EnableSensitiveDataLogging() // Для отладки
                      .EnableDetailedErrors();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка глобального конвертера для DateTime в UTC
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>(
                        v => v.Kind == DateTimeKind.Unspecified ? v.ToUniversalTime() : DateTime.SpecifyKind(v, DateTimeKind.Utc),
                        v => v));
                }
            }
        }

        // Настройка таблиц и столбцов
        modelBuilder.Entity<Person>().ToTable("person").Property(p => p.Id).HasColumnName("id");
        modelBuilder.Entity<Person>().ToTable("person").Property(p => p.Login).HasColumnName("login");
        modelBuilder.Entity<Person>().ToTable("person").Property(p => p.PasswordHash).HasColumnName("password_hash");
        modelBuilder.Entity<Person>().ToTable("person").Property(p => p.RoleId).HasColumnName("role_id");
        modelBuilder.Entity<Person>().ToTable("person").Property(p => p.CreatedAt).HasColumnName("created_at");
        modelBuilder.Entity<Person>().ToTable("person").Property(p => p.DeletedAt).HasColumnName("deleted_at");

        modelBuilder.Entity<Role>().ToTable("role").Property(r => r.Id).HasColumnName("id");
        modelBuilder.Entity<Role>().ToTable("role").Property(r => r.Name).HasColumnName("name");

        modelBuilder.Entity<Client>().ToTable("client").Property(c => c.Id).HasColumnName("id");
        modelBuilder.Entity<Client>().ToTable("client").Property(c => c.PersonId).HasColumnName("person_id");
        modelBuilder.Entity<Client>().ToTable("client").Property(c => c.ContactInfo).HasColumnName("contact_info");
        modelBuilder.Entity<Client>().ToTable("client").Property(c => c.IncomeLevelId).HasColumnName("income_level_id");

        modelBuilder.Entity<Service>().ToTable("service").Property(s => s.Id).HasColumnName("id");
        modelBuilder.Entity<Service>().ToTable("service").Property(s => s.TypeId).HasColumnName("type_id");
        modelBuilder.Entity<Service>().ToTable("service").Property(s => s.Name).HasColumnName("name");
        modelBuilder.Entity<Service>().ToTable("service").Property(s => s.Description).HasColumnName("description");
        modelBuilder.Entity<Service>().ToTable("service").Property(s => s.Price).HasColumnName("price");
        modelBuilder.Entity<Service>().ToTable("service").Property(s => s.IsAvailable).HasColumnName("is_available");

        modelBuilder.Entity<Cart>().ToTable("cart").Property(c => c.Id).HasColumnName("id");
        modelBuilder.Entity<Cart>().ToTable("cart").Property(c => c.ClientId).HasColumnName("client_id");
        modelBuilder.Entity<Cart>().ToTable("cart").Property(c => c.ServiceId).HasColumnName("service_id");
        modelBuilder.Entity<Cart>().ToTable("cart").Property(c => c.AddedAt).HasColumnName("added_at");

        modelBuilder.Entity<Order>().ToTable("order").Property(o => o.Id).HasColumnName("id");
        modelBuilder.Entity<Order>().ToTable("order").Property(o => o.ClientId).HasColumnName("client_id");
        modelBuilder.Entity<Order>().ToTable("order").Property(o => o.Date).HasColumnName("date");
        modelBuilder.Entity<Order>().ToTable("order").Property(o => o.TotalPrice).HasColumnName("total_price");
        modelBuilder.Entity<Order>().ToTable("order").Property(o => o.StatusId).HasColumnName("status_id");

        modelBuilder.Entity<Event>().ToTable("event").Property(e => e.Id).HasColumnName("id");
        modelBuilder.Entity<Event>().ToTable("event").Property(e => e.ServiceId).HasColumnName("service_id");
        modelBuilder.Entity<Event>().ToTable("event").Property(e => e.Date).HasColumnName("date");
        modelBuilder.Entity<Event>().ToTable("event").Property(e => e.Duration).HasColumnName("duration");
        modelBuilder.Entity<Event>().ToTable("event").Property(e => e.AverageAge).HasColumnName("average_age");
        modelBuilder.Entity<Event>().ToTable("event").Property(e => e.TypeId).HasColumnName("type_id");
        modelBuilder.Entity<Event>().ToTable("event").Property(e => e.Description).HasColumnName("description");
        modelBuilder.Entity<Event>().ToTable("event").Property(e => e.AnimatorId).HasColumnName("animator_id");

        modelBuilder.Entity<GameMachine>().ToTable("game_machine").Property(g => g.Id).HasColumnName("id");
        modelBuilder.Entity<GameMachine>().ToTable("game_machine").Property(g => g.Name).HasColumnName("name");
        modelBuilder.Entity<GameMachine>().ToTable("game_machine").Property(g => g.StatusId).HasColumnName("status_id");
        modelBuilder.Entity<GameMachine>().ToTable("game_machine").Property(g => g.LastUpdated).HasColumnName("last_updated");
        modelBuilder.Entity<GameMachine>().ToTable("game_machine").Property(g => g.UpdatedById).HasColumnName("updated_by_id");

        modelBuilder.Entity<MachineStatus>().ToTable("machine_status").Property(m => m.Id).HasColumnName("id");
        modelBuilder.Entity<MachineStatus>().ToTable("machine_status").Property(m => m.Name).HasColumnName("name");

        // Конфигурация связей
        modelBuilder.Entity<Person>()
            .HasOne(p => p.Role)
            .WithMany()
            .HasForeignKey(p => p.RoleId);

        modelBuilder.Entity<Client>()
            .HasOne(c => c.Person)
            .WithMany()
            .HasForeignKey(c => c.PersonId);

        modelBuilder.Entity<Cart>()
            .HasOne(c => c.Client)
            .WithMany()
            .HasForeignKey(c => c.ClientId);

        modelBuilder.Entity<Cart>()
            .HasOne(c => c.Service)
            .WithMany()
            .HasForeignKey(c => c.ServiceId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Client)
            .WithMany()
            .HasForeignKey(o => o.ClientId);

        modelBuilder.Entity<Event>()
            .HasOne(e => e.Animator)
            .WithMany()
            .HasForeignKey(e => e.AnimatorId);

        modelBuilder.Entity<GameMachine>()
            .HasOne(g => g.Status)
            .WithMany()
            .HasForeignKey(g => g.StatusId);
    }
}