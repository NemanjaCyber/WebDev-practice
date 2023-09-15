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

    [HttpPost("DodajFabriku")]
    public async Task<ActionResult> DodajFabriku([FromBody]Fabrika fabrika)
    {
        try
        {
            Context.Fabrike!.Add(fabrika);
            await Context.SaveChangesAsync();
            return Ok(fabrika);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajSilos/{idFabrike}/{oznaka}/{kapacitet}/{trenutnaKolicina}")]
    public async Task<ActionResult> DodajSilos(int idFabrike,string oznaka,int kapacitet, int trenutnaKolicina)
    {
        var pom=Context.Silosi!.Where(p=>p.Oznaka==oznaka && p.MojaFabrika!.ID==idFabrike).FirstOrDefault();
        if(pom!=null)
        {
            return BadRequest("Silos sa ovom oznakom vec postoji!");
        }

        if(kapacitet!=2000)
        {
            return BadRequest("Nevalidan kapacitet. Mora biti 2000t.");
        }

        if(trenutnaKolicina>kapacitet || trenutnaKolicina<0)
        {
            return BadRequest("Nevalidna trenutna kolicina.");
        }

        var s=new Silos();
        s.Oznaka=oznaka;
        s.Kapacitet=kapacitet;
        s.TrenutnaPopunjenost=trenutnaKolicina;
        s.MojaFabrika=await Context.Fabrike!.FindAsync(idFabrike);
    
        try
        {
            Context.Silosi!.Add(s);
            await Context.SaveChangesAsync();
            return Ok(s);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [HttpGet("VratiFabrike")]
    public async Task<ActionResult> VratiFabrike()
    {
        var fabrike=await Context.Fabrike!
            .Include(p=>p.Silosi)
            .Select(q=>new {
                id=q.ID,
                naziv=q.Naziv,
                oznake=q.Silosi!.Select(r=>r.Oznaka),
                kapaciteti=q.Silosi!.Select(r=>r.Kapacitet),
                trenutneKolicine=q.Silosi!.Select(r=>r.TrenutnaPopunjenost)
            }).ToListAsync();
        return Ok(fabrike);
    }

    [HttpPut("SipajUSilos/{idSilosa}/{kolicina}")]
    public async Task<ActionResult> SipajUSilos(int idSilosa, int kolicina)
    {
        var s=Context.Silosi!.Where(p=>p.ID==idSilosa).FirstOrDefault();
        if(s!.TrenutnaPopunjenost + kolicina > s.Kapacitet)
        {
            return BadRequest("Nema dovoljno mesta");
        }

        s.TrenutnaPopunjenost+=kolicina;

        try
        {
            Context.Silosi!.Update(s);
            await Context.SaveChangesAsync();
            return Ok(s);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
