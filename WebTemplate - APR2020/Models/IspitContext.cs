namespace Models;

public class IspitContext : DbContext
{
    public DbSet<VideoKlub>? Klubovi {get;set;}
    public DbSet<Polica>? Police {get;set;}
    public IspitContext(DbContextOptions options) : base(options)
    {
        
    }
}
