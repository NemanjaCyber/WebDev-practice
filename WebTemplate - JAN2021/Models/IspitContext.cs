namespace Models;

public class IspitContext : DbContext
{
    public DbSet<Grad>? Gradovi {get; set;}
    public DbSet<MeteoroloskiPodaci>? MeteoroloskiPodaci {get;set;}
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
