namespace Models;

public class IspitContext : DbContext
{
    public DbSet<Ptica>? Ptice { get; set; }
    public DbSet<NepoznataPtica>? Nepoznate { get; set; }
    public DbSet<Osobina>? Osobine { get; set; }
    public DbSet<Podrucje>? Podrucja { get; set; }
    public DbSet<Vidjenje>? Vidjenja { get; set; }

    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
