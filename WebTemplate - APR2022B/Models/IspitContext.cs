namespace Models;

public class IspitContext : DbContext
{
    public DbSet<Prodavnica>? Prodavnice {get;set;}
    public DbSet<Sastojak>? Sastojci {get;set;}
    public DbSet<Sto>? Stolovi {get;set;}
    public DbSet<Spoj>? Spojevi {get;set;}
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
