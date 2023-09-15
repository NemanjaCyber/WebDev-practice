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

    [HttpGet("VratiSveSilose")]
    public async Task<ActionResult> VratiSveSilose()
    {
        var podaci=await Context.Silosi!
            .Select(p=>new{
                idSilos=p.ID,
                trenutnaPopunjenost=p.TrenutnaPopunjenost,
                kapacitet=p.Kapacitet,
                vlaznost=p.Vlaznost
            }).ToListAsync();
        
        return Ok(podaci);
    }

    [HttpGet("VratiOznake")]
    public async Task<ActionResult> VratiOznakeIImena()
    {
        var podaci=await Context.Silosi!
            .Select(x=>new{
                id=x.ID,
                oznake=x.Oznaka
            }).ToListAsync();
        
        return Ok(podaci);
    }
    
    [HttpGet("VratiRadnike")]
    public async Task<ActionResult> VratiRadnike()
    {
        var podaci=await Context.Radnici!
            .Select(x=>new{
                radnikId=x.ID,
                imena=x.ImePrezime
            }).ToListAsync();
        
        return Ok(podaci);
    }

    [HttpPut("DodajKolicinu/{kolicina}/{silosID}")]
    public async Task<ActionResult> DodajKolicinu(int kolicina, int silosID)
    {
        var s=await Context.Silosi!.Where(p=>p.ID==silosID).FirstOrDefaultAsync();

        if(s!.TrenutnaPopunjenost+kolicina>s.Kapacitet)
        {
            return BadRequest("Nema mesta za tu kolicinu");
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

    [HttpPut("IzmeniVlaznost/{vlaznost}/{silosID}")]
    public async Task<ActionResult> IzmeniVlaznost(int vlaznost, int silosID)
    {
        var s=await Context.Silosi!.Where(p=>p.ID==silosID).FirstOrDefaultAsync();

        if(vlaznost<0||vlaznost>100)
        {
            return BadRequest("Nevalidna vrednost za vlaznost");
        }

        s!.Vlaznost=vlaznost;

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

    [HttpPut("IzmeniKolicinuIVlaznost/{kolicina}/{vlaznost}/{silosID}")]
    public async Task<ActionResult> IzmeniKolicinuIVlaznost(int kolicina, int vlaznost, int silosID)
    {
        var s=await Context.Silosi!.Where(p=>p.ID==silosID).FirstOrDefaultAsync();

        if(s!.TrenutnaPopunjenost+kolicina>s.Kapacitet||vlaznost<0||vlaznost>100)
        {
            return BadRequest("Nevalidna vrednost za vlaznost");
        }

        s.TrenutnaPopunjenost+=kolicina;
        s!.Vlaznost=vlaznost;

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

    [HttpPost("IsprazniSilos/{radnikID}/{silosID}")]
    public async Task<ActionResult> IsprazniSilos(int radnikID, int silosID)
    {
        //var r=await Context.Radnici!.Where(p=>p.ID==radnikID).FirstOrDefaultAsync();
        var s=await Context.Silosi!.Where(q=>q.ID==silosID).FirstOrDefaultAsync();

        var praznjenje=new Praznjenje();
        praznjenje.Radnik= await Context.Radnici!.Where(x=>x.ID==radnikID).FirstOrDefaultAsync();//praznjenje.Radnik=r;
        praznjenje.Silos=await Context.Silosi!.Where(y=>y.ID==silosID).FirstOrDefaultAsync();//praznjenje.Silos=s;
        praznjenje.IspraznjenaKolicina=await Context.Silosi!.Where(z=>z.ID==silosID).Select(p=>p.TrenutnaPopunjenost).FirstOrDefaultAsync();
        s!.TrenutnaPopunjenost=0;

        try
        {
            Context.Praznjenja!.Add(praznjenje);
            Context.Silosi!.Update(s);
            await Context.SaveChangesAsync();
            return Ok(praznjenje);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
