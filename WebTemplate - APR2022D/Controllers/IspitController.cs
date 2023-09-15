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

    [HttpGet("VratiProdavniceSaTipovimaIBrendovima")]
    public async Task<ActionResult> VratiProdavniceSaTipovimaIBrendovima()
    {
        var podaci=await Context.Prodavnice!
            .Include(p=>p.Komponente)
            .Select(x=>new{
                id=x.ID,
                naziv=x.Naziv,
                tipovi=(x.Komponente!.Select(z=>z.Tip)).Distinct(),
                brendovi=(x.Komponente!.Select(z=>z.Brend)).Distinct()
            }).ToListAsync();

        return Ok(podaci);
    }

    [HttpGet("PretraziPoTipu/{tip}/{prodavnicaId}")]
    public async Task<ActionResult> PretraziPoTipu(string tip, int prodavnicaId)
    {
        var podaci=await Context.Prodavnice!
            .Include(p=>p.Komponente)
            .Where(q=>q.ID==prodavnicaId)
            .Select(x=>new{
                podaci=x.Komponente!.Where(s=>s.Tip==tip)
            }).ToListAsync();
        
        return Ok(podaci);
    }

    [HttpGet("PretraziPoTipuIBrendu/{tip}/{brend}/{prodavnicaId}")]
    public async Task<ActionResult> PretraziPoTipuIBrendu(string tip, string brend, int prodavnicaId)
    {
        var podaci=await Context.Prodavnice!
            .Include(p=>p.Komponente)
            .Where(q=>q.ID==prodavnicaId)
            .Select(x=>new{
                podaci=x.Komponente!.Where(o=>o.Tip==tip && o.Brend==brend)
            }).ToListAsync();

        return Ok(podaci);
    }

    [HttpGet("PretraziPoSvemu/{tip}/{brend}/{cenaOd}/{cenaDo}/{prodavnicaId}")]
    public async Task<ActionResult> PretraziPoSvemu(string tip, string brend, int cenaOd, int cenaDo, int prodavnicaId)
    {
        var podaci=await Context.Prodavnice!
            .Include(p=>p.Komponente)
            .Where(q=>q.ID==prodavnicaId)
            .Select(x=>new{
                podaci=x.Komponente!.Where(o=>o.Tip==tip && o.Brend==brend && o.Cena>=cenaOd && o.Cena<=cenaDo)
            }).ToListAsync();
        
        return Ok(podaci);
    }

    [HttpPut("Kupi/{sifra}/{kolicina}")]
    public async Task<ActionResult> Kupi( int sifra, int kolicina)
    {
        var k=await Context.Komponente!.Where(p=>p.Sifra==sifra).FirstOrDefaultAsync();
        if(k==null){
            return BadRequest("Nepostojeca komponenta");
        }

        if(k!.Kolicina<kolicina)
        {
            return BadRequest("Nema dovoljno na stanju");
        }

        k.Kolicina-=kolicina;

        try
        {
            Context.Komponente!.Update(k);
            await Context.SaveChangesAsync();
            return Ok(k);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
