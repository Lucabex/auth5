using Microsoft.EntityFrameworkCore;
using auth5.Models;

namespace auth5.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<User>User {get;set;}
}