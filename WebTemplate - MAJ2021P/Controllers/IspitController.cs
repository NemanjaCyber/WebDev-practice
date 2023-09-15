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

    [HttpGet("PreuzmiFabrikeSaSilosima")]
    public async Task<ActionResult> PreuzmiFabrikeSaSilosima()
    {
        var podaci=await Context.Fabrike!
            .Include(p=>p.Silosi)
            .Select(x=>new{
                id=x.ID,
                naziv=x.Naziv,
                silosi=x.Silosi!.Select(o=>new{
                    idSilos=o.ID,
                    oznaka=o.Oznaka,
                    kapacitet=o.Kapacitet,
                    trenutnaPopunjenost=o.TrenutnaPopunjenost
                })
            }).ToListAsync();
        
        return Ok(podaci);
    }

    [HttpPut("SipajUSilos/{idSilos}/{kolicina}")]
    public async Task<ActionResult> SipajUSilos(int idSilos, int kolicina)
    {
        var silos=await Context.Silosi!
            .Where(p=>p.ID==idSilos)
            .FirstOrDefaultAsync();

        if(silos!.TrenutnaPopunjenost+kolicina>silos.Kapacitet)
        {
            return BadRequest("Nema mesta u silosu za tu kolicinu.");
        }

        silos.TrenutnaPopunjenost+=kolicina;

        try
        {
            Context.Silosi!.Update(silos);
            await Context.SaveChangesAsync();
            return Ok(silos);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
