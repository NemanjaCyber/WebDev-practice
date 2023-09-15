namespace Models;

public class IspitContext : DbContext
{
    public DbSet<Prodavnica>? Prodavnice {get;set;}
    public DbSet<Artikl>? Artikli {get;set;}
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
