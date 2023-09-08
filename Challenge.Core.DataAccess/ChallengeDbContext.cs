using Challenge.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Challenge.Core.DataAccess;

public class ChallengeDbContext: DbContext
{
    
    private readonly string _connectionString;

    public ChallengeDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public ChallengeDbContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Challenge");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        options.UseSqlite(_connectionString);
    }
    
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Product> Products { get; set; }
    
    public DbSet<Order> Orders { get; set; } 
}