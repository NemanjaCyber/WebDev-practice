namespace Models;

public class IspitContext : DbContext
{
    public DbSet<Fabrika>? Fabrike {get;set;}
    public DbSet<Silos>? Silosi {get;set;}
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
