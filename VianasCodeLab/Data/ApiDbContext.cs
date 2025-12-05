using Microsoft.EntityFrameworkCore;
using VianasCodeLab.Model;

namespace VianasCodeLab.Data;


public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Computer> Computer { get; set; }

    public DbSet<Component> Component { get; set; }
}
