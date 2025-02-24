using Microsoft.EntityFrameworkCore;
using HibaVonal.DataContext.Entities;

namespace HibaVonal.DataContext;

public class SQL : DbContext
{
    public SQL(DbContextOptions options) : base(options) { }

    public DbSet<Role> Role { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Address> Address { get; set; }
    public DbSet<Dormitory> Dormitory { get; set; }
    public DbSet<ErrorType> ErrorType { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<PersonalRoom> PersonalRoom { get; set; }
    public DbSet<SharedRoom> SharedRoom { get; set; }
    public DbSet<ErrorLog> ErrorLog { get; set; }
}
