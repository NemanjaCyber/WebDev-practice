namespace Models;

public class IspitContext : DbContext
{
    public DbSet<Igrac>? Igraci {get;set;}
    public DbSet<Mec>? Mecevi {get;set;}
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
