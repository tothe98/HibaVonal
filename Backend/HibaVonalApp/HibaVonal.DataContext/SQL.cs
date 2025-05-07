using Microsoft.EntityFrameworkCore;
using HibaVonal.DataContext.Entities;
using Hibavonal.DataContext.Entities;
using System.Reflection.Metadata;

namespace HibaVonal.DataContext;

public class SQL : DbContext
{
    public SQL(DbContextOptions options) : base(options) { }

    public DbSet<Role> Role { get; set; }
    public DbSet<Room> Room { get; set; }
    public DbSet<PersonalRoom> PersonalRoom { get; set; }
    public DbSet<SharedRoom> SharedRoom { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Address> Address { get; set; }
    public DbSet<Dormitory> Dormitory { get; set; }
    public DbSet<ErrorType> ErrorType { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<ErrorLog> ErrorLog { get; set; }
    public DbSet<UserRole> UserRole { get; set; }

    public DbSet<RoomEquipment> RoomEquipment { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>()
            .HasDiscriminator<string>("RoomType")
            .HasValue<PersonalRoom>("PersonalRoom")
            .HasValue<SharedRoom>("SharedRoom");

        modelBuilder.Entity<UserRole>().HasKey("UserId", "RoleId");

        modelBuilder.Entity<RoomEquipment>()
        .HasKey(re => new { re.RoomId, re.EquipmentId });

        modelBuilder.Entity<RoomEquipment>()
            .HasOne(re => re.Room)
            .WithMany(r => r.RoomEquipment)
            .HasForeignKey(re => re.RoomId);

        modelBuilder.Entity<RoomEquipment>()
            .HasOne(re => re.Equipment)
            .WithMany(e => e.RoomEquipments)
            .HasForeignKey(re => re.EquipmentId);


    }
}
