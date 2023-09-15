namespace Models;

public class IspitContext : DbContext
{
    public DbSet<Silos>? Silosi {get;set;}
    public DbSet<Radnik>? Radnici {get;set;}
    public DbSet<Praznjenje>? Praznjenja {get;set;}
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
