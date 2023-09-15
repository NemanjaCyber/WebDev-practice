namespace Models;

public class IspitContext : DbContext
{
    public DbSet<Biljka>? Biljke {get;set;}
    public DbSet<Sajt>? Sajtovi {get;set;}
    public DbSet<NepoznataBiljka>? NepoznateBiljke {get;set;}
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
