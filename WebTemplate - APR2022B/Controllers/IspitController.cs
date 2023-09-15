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

    [HttpGet("VratiProdavniceSaSastojcima")]
    public async Task<ActionResult> VratiProdavniceSaSastojcima()
    {
        var podaci=await Context.Prodavnice!
            .Include(p=>p.Sastojci)
            .Select(x=>new{
                prodavnicaId=x.ID,
                prodavnicaNaziv=x.Naziv,
                prodavnicaZarada=x.Zarada,
                sastojci=x.Sastojci!.Select(y=>y.Naziv)
            }).ToListAsync();
        
        return Ok(podaci);

    }

    [HttpGet("VratiStolove/{prodavnicaId}")]
    public async Task<ActionResult> VratiStolove(int prodavnicaId)
    {
        var podaci=await Context.Prodavnice!
            .Include(p=>p.Stolovi)
            .Where(q=>q.ID==prodavnicaId)
            .Select(x=>new{
                stoOznaka=x.Stolovi!.Select(y=>y.Oznaka)
            }).ToListAsync();
        
        return Ok(podaci);
    }

    [HttpGet("VratiSveStoloveZaPrikaz/{prodavnicaId}")]
    public async Task<ActionResult> VratiSveStoloveZaPrikaz(int prodavnicaId)
    {
        var podaci=await Context.Prodavnice!
            .Include(p=>p.Stolovi)
            .Where(q=>q.ID==prodavnicaId)
            .Select(x=>new{
                stoId=x.Stolovi!.Select(z=>z.ID),
                stoOznaka=x.Stolovi!.Select(t=>t.Oznaka),
                stoVrednost=x.Stolovi!.Select(i=>i.VrednostStola)
            }).ToListAsync();
        
        return Ok(podaci);
    }

    [HttpPost("DodajSastojakNaSto/{stoOznaka}/{sastojak}/{kolicina}")]
    public async Task<ActionResult> DodajSastojakNaSto(string stoOznaka, string sastojak,int kolicina)
    {
        var spoj=new Spoj();
        spoj.Sastojak=await Context.Sastojci!.Where(p=>p.Naziv==sastojak).FirstOrDefaultAsync();
        spoj.Sto=await Context.Stolovi!.Where(z=>z.Oznaka==stoOznaka).FirstOrDefaultAsync();

        var sto=await Context.Stolovi!.Where(z=>z.Oznaka==stoOznaka).FirstOrDefaultAsync();
        var sastojakk=await Context.Sastojci!.Where(p=>p.Naziv==sastojak).FirstOrDefaultAsync();
        sto!.VrednostStola+=sastojakk!.Cena*kolicina;

        try
        {
            Context.Spojevi!.Add(spoj);
            Context.Stolovi!.Update(sto);
            await Context.SaveChangesAsync();
            return Ok(sto);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut("Isporuci/{stoOznaka}/{prodavnicaId}")]
    public async Task<ActionResult> Isporuci(string stoOznaka, int prodavnicaId)
    {
        var sto=await Context.Stolovi!.Where(p=>p.Oznaka==stoOznaka).FirstOrDefaultAsync();
        var prodavnica=await Context.Prodavnice!.Where(q=>q.ID==prodavnicaId).FirstOrDefaultAsync();

        prodavnica!.Zarada+=sto!.VrednostStola;
        sto!.VrednostStola=0;

        try
        {
            Context.Prodavnice!.Update(prodavnica);
            Context.Stolovi!.Update(sto);
            await Context.SaveChangesAsync();
            return Ok(sto);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
