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

    [HttpGet("VratiSajtoveSaParametrima")]
    public async Task<ActionResult> VratiSajtoveSaParametrima()
    {
        // var podaci=await Context.Sajtovi!
        //     .Include(p=>p.Biljke)
        //     .Select(q=>new{
        //         id=q.ID,
        //         naziv=q.Naziv,
        //         biljke=q.Biljke
        //     }).ToListAsync();

        // return Ok(podaci);

        var podaci=await Context.Sajtovi!
            .Include(p=>p.Biljke)
            .Select(q=>new{
                id=q.ID,
                naziv=q.Naziv,
                podrucja=q.Biljke!.Select(x=>x.Podrucje),
                cvetovi=q.Biljke!.Select(x=>x.Cvet),
                listovi=q.Biljke!.Select(x=>x.List),
                stabla=q.Biljke!.Select(x=>x.Stablo)
            }).ToListAsync();
        return Ok(podaci);
    }

    [HttpGet("Pretrazi/{podrucje}/{cvet}/{list}/{stablo}/{sajtId}")]
    public async Task<ActionResult> Pretrazi(string podrucje, string cvet, string list, string stablo, int sajtId)
    {
        var biljke=await Context.Sajtovi!
            .Include(p=>p.Biljke)
            .Where(q=>q.ID==sajtId)
            .Select(x=>new{
                biljke=x.Biljke!.Where(z=>z.Podrucje==podrucje && z.Cvet==cvet && z.List==list && z.Stablo==stablo)
            }).ToListAsync();
        
        return Ok(biljke);
    }

    [HttpPost("DodajNepoznatuBiljku/{podrucje}/{cvet}/{list}/{stablo}/{sajtId}")]
    public async Task<ActionResult> DodajNepoznatuBiljku(string podrucje, string cvet, string list, string stablo, int sajtId)
    {
            var nepoznata=new NepoznataBiljka();
            nepoznata.Podrucje=podrucje;
            nepoznata.Cvet=cvet;
            nepoznata.List=list;
            nepoznata.Stablo=stablo;
            nepoznata.Sajt=await Context.Sajtovi!.Where(i=>i.ID==sajtId).FirstOrDefaultAsync();
            try
            {
                Context.NepoznateBiljke!.Add(nepoznata);
                await Context.SaveChangesAsync();
                return Ok(nepoznata);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        
    }

    [HttpPut("DodajVidjenje/{biljkaId}")]
    public async Task<ActionResult> DodajVidjenje(int biljkaId)
    {
        var b=await Context.Biljke!.Where(p=>p.ID==biljkaId).FirstOrDefaultAsync();
        b!.Vidjanja++;

        try
        {
            Context.Biljke!.Update(b);
            await Context.SaveChangesAsync();
            return Ok(b);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }

    }
}

