namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class IspitController : ControllerBase
{
    public IspitContext Context { get; set; }

    public IspitController(IspitContext context)
    {
        Context = context;
    }

     [HttpGet("VratiProdavniceSaBrendovima")]
     public async Task<ActionResult> VratiProdavniceSaBrendovima()
     {
         var podaci=await Context.Prodavnice!
             .Include(p=>p.Artikli)
             .Select(x=>new{
                 id=x.ID,
                 naziv=x.Naziv,
                 brendovi=x.Artikli!.Select(o=>o.Brend)
             }).ToListAsync();
         return Ok(podaci);
     }

    [HttpGet("VratiArtikleZaBrend/{brend}/{prodavnicaId}")]
    public async Task<ActionResult> VratiArtikleZaBrend(string brend, int prodavnicaId)
    {
        var artikli=await Context.Prodavnice!
            .Include(p=>p.Artikli)
            .Where(q=>q.ID==prodavnicaId)
            .Select(x=>new{
                artikli=x.Artikli!.Where(o=>o.Brend==brend).Select(k=>new{
                    idArtikl=k.ID,
                    sifra=k.Sifra,
                    kolicina=k.Kolicina,
                    cena=k.Cena
                })
            }).ToListAsync();
        
        return Ok(artikli);
    }

    [HttpGet("VratiArtiklePoCeni/{minCena}/{maxCena}/{brend}/{prodavnicaId}")]
    public async Task<ActionResult> VratiArtiklePoCeni(int minCena, int maxCena, string brend, int prodavnicaId)
    {
        var artikli=await Context.Prodavnice!
            .Include(p=>p.Artikli)
            .Where(q=>q.ID==prodavnicaId)
            .Select(x=>new{
                artikli=x.Artikli!.Where(o=>o.Brend==brend && o.Cena>=minCena && o.Cena<=maxCena).Select(k=>new{
                    idArtikl=k.ID,
                    sifra=k.Sifra,
                    kolicina=k.Kolicina,
                    cena=k.Cena
                })
            }).ToListAsync();
        
        return Ok(artikli);
    }

    [HttpGet("VratiArtiklePoVelicini/{velicina}/{brend}/{prodavnicaId}")]
    public async Task<ActionResult> VratiArtiklePoVelicini(string velicina,string brend, int prodavnicaId)
    {
        var artikli=await Context.Prodavnice!
            .Include(p=>p.Artikli)
            .Where(q=>q.ID==prodavnicaId)
            .Select(x=>new{
                artikli=x.Artikli!.Where(o=>o.Brend==brend && o.Velicina==velicina).Select(k=>new{
                    idArtikl=k.ID,
                    sifra=k.Sifra,
                    kolicina=k.Kolicina,
                    cena=k.Cena
                })
            }).ToListAsync();
        
        return Ok(artikli);
    }

    [HttpGet("VratiArtikleZaSve/{velicina}/{minCena}/{maxCena}/{brend}/{prodavnicaId}")]
    public async Task<ActionResult> VratiArtikleZaSve(string velicina, int minCena, int maxCena, string brend, int prodavnicaId)
    {
        var artikli=await Context.Prodavnice!
            .Include(p=>p.Artikli)
            .Where(q=>q.ID==prodavnicaId)
            .Select(x=>new{
                artikli=x.Artikli!.Where(o=>o.Brend==brend && o.Velicina==velicina && o.Cena>minCena && o.Cena<=maxCena).Select(k=>new{
                    idArtikl=k.ID,
                    sifra=k.Sifra,
                    kolicina=k.Kolicina,
                    cena=k.Cena
                })
            }).ToListAsync();
        
        return Ok(artikli);
    }

    [HttpPut("KupiArtikl/{artiklId}")]
    public async Task<ActionResult> KupiArtikl(int artiklId)
    {
        var a=await Context.Artikli!.Where(p=>p.ID==artiklId).FirstOrDefaultAsync();

        a!.Kolicina--;

        try
        {
            Context.Artikli!.Update(a);
            await Context.SaveChangesAsync();
            return Ok(a);

        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
