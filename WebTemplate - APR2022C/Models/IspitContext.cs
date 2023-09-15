namespace Models;

public class IspitContext : DbContext
{
    public DbSet<Iverica>? Iverice {get;set;}
    public DbSet<Prodavnica>? Prodavnice {get;set;}
    public DbSet<Magacin>? Otpadci {get;set;}
    public DbSet<NarucenePloce>? NarucenePloce {get;set;}
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
