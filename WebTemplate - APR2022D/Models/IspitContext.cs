namespace Models;

public class IspitContext : DbContext
{
    public DbSet<Prodavnica>? Prodavnice {get;set;}
    public DbSet<Komponenta>? Komponente {get;set;}
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
