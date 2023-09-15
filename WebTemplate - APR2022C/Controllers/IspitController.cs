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

    [HttpGet("VratiSveProdavniceSaSarama")]
    public async Task<ActionResult> VratiSveProdavniceSaSarama()
    {
        var podaci=await Context.Prodavnice!
            .Include(p=>p.Iverice)
            .Select(x=>new{
                id=x.ID,
                naziv=x.Naziv,
                zarada=x.Zarada,
                sare=x.Iverice!.Select(o=>o.Sara)
            }).ToListAsync();

        return Ok(podaci);
    }

    [HttpPut("NaruciPlocu/{sara}/{sirina}/{duzina}")]
    public async Task<ActionResult> NaruciPlocu(string sara,float sirina, float duzina)
    {
        if(sirina>1 ||duzina>2)
        {
            return BadRequest("Nevalidne vrednosti duzine i sirine. Max duz: 2m. Max sir: 1m");
        }

        var prvoOdOdpadaka=await Context.Otpadci!.Where(p=>p.Sara==sara && p.Sirina==sirina && p.Duzina==duzina).FirstOrDefaultAsync();

        if(prvoOdOdpadaka!=null)
        {
            Context.Otpadci!.Remove(prvoOdOdpadaka);
            await Context.SaveChangesAsync();
            return Ok(prvoOdOdpadaka);
        }

        var ploca=await Context.Iverice!.Where(p=>p.Sara==sara).FirstOrDefaultAsync();

        if(ploca==null)
        {
            return BadRequest("Ploca sa navedenom sarom ne postoji");
        }

        if(ploca.Kolicina<1)
        {
            return BadRequest("Nema na stanju ploca sa ovakvom sarom");
        }

        var duzinaOdpadak=ploca.Duzina-duzina;
        var sirinaOdpadak=ploca.Sirina-sirina;

        var povrsinaOdpadak=duzinaOdpadak*sirinaOdpadak;

        var narucenaPloca=new NarucenePloce();
        narucenaPloca.Sara=ploca.Sara;
        narucenaPloca.Duzina=duzina;
        narucenaPloca.Sirina=sirina;
        narucenaPloca.Iverica=ploca;
        narucenaPloca.Cena=ploca.Cena;
        ploca.Kolicina--;

        if(povrsinaOdpadak>=0.4)
        {
            var odpadak=new Magacin();
            odpadak.Duzina=duzinaOdpadak;
            odpadak.Sirina=sirinaOdpadak;
            odpadak.Cena=ploca.Cena;
            odpadak.Iverica=ploca;
            odpadak.Sara=ploca.Sara;

            try
            {
                Context.Otpadci!.Add(odpadak);
                Context.NarucenePloce!.Add(narucenaPloca);
                Context.Iverice!.Update(ploca);
                await Context.SaveChangesAsync();
                return Ok(odpadak);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        try
        {
            Context.NarucenePloce!.Add(narucenaPloca);
            Context.Iverice!.Update(ploca);
            await Context.SaveChangesAsync();
            return Ok(narucenaPloca);

        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }

    }

}
