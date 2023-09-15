namespace Models;

public class IspitContext : DbContext
{
    public DbSet<Film>? Filmovi{get;set;}
    public DbSet<ProdKuca>? ProdukcijskeKuce{get;set;}
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
