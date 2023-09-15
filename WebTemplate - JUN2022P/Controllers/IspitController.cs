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

    [HttpGet("VratiProdavniceSaMarkama")]
    public async Task<ActionResult> VratiPodavniceSaMarkama()
    {
        var podaci=await Context.Prodavnice!
            .Include(p=>p.Automobili)
            .Select(q=>new{
                prodavnicaID=q.ID,
                marke=q.Automobili!.Select(x=>x.Marka)
            }).ToListAsync();
        return Ok(podaci);
    }

    [HttpGet("VratiModeleZaMarku/{marka}/{prodavnicaID}")]
    public async Task<ActionResult> VratiModeleZaMarku(string marka,int prodavnicaID)
    {
        var modeli=await Context.Prodavnice!
            .Include(p=>p.Automobili)
            .Where(q=>q.ID==prodavnicaID)
            .Select(x=>new{
                modeli=x.Automobili!.Where(r=>r.Marka==marka).Select(c=>c.Model)
            }).ToListAsync();
        
        return Ok(modeli);
    }

    [HttpGet("VratiBojeZaMarkuModel/{marka}/{model}/{prodavnicaID}")]
    public async Task<ActionResult> VratiBojeZaMarkuModel(string marka,string model,int prodavnicaID)
    {
        var boje=await Context.Prodavnice!
            .Include(p=>p.Automobili)
            .Where(q=>q.ID==prodavnicaID)
            .Select(x=>new{
                boje=x.Automobili!.Where(o=>o.Marka==marka && o.Model==model).Select(k=>k.Boja)
            }).ToListAsync();

        return Ok(boje);
    }

    [HttpGet("VratiPoPretrazi/{marka}/{model}/{boja}/{prodavnicaID}")]
    public async Task<ActionResult> VratiPoPretrazi(string marka, string model, string boja, int prodavnicaID)
    {
        //ovo %2F je kod za /. / pisem kad nemam input za model ili boju
        if(model=="%2F" && boja=="%2F")
        {
            var automobili1=await Context.Prodavnice!
                .Include(p=>p.Automobili)
                .Where(q=>q.ID==prodavnicaID)
                .Select(x=>x.Automobili!.Where(h=>h.Marka==marka))
                .ToListAsync();
            return Ok(automobili1);
        }
        if(boja=="%2F")
        {
            var automobili2=await Context.Prodavnice!
                .Include(p=>p.Automobili)
                .Where(q=>q.ID==prodavnicaID)
                .Select(x=>x.Automobili!.Where(o=>o.Marka==marka && o.Model==model))
                .ToListAsync();
            return Ok(automobili2);
        }
        
        var automobili3=await Context.Prodavnice!
            .Include(p=>p.Automobili)
            .Where(q=>q.ID==prodavnicaID)
            .Select(x=>x.Automobili!.Where(o=>o.Marka==marka&&o.Model==model&&o.Boja==boja))
            .ToListAsync();
        return Ok(automobili3);
    }

    [HttpPut("KupiAutomobil/{automobilID}")]
    public async Task<ActionResult> KupiAutomobil(int automobilID)
    {
        var auto=await Context.Automobili!
            .Where(p=>p.ID==automobilID).FirstOrDefaultAsync();

        if(auto!.Kolicina==0)
        {
            return BadRequest("Nema vise na stanju");
        }
        
        auto.Kolicina--;
        auto.DatumPoslednjeProdaje=DateTime.Now.ToShortDateString();

        try
        {
            Context.Automobili!.Update(auto);
            await Context.SaveChangesAsync();
            return Ok(auto);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
