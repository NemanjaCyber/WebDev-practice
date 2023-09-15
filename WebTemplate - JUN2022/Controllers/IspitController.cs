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

    [HttpPost("DodajProdavnicu/{naziv}")]
    public async Task<ActionResult> dodajProdavnicu(string naziv)
    {
        var p= Context.Prodavnice!.Where(p=>p.Naziv==naziv).FirstOrDefault();
        if(p!=null)
        {
            return BadRequest("Prodavnice vec postoji");
        }
        Prodavnica pp=new Prodavnica();
        pp.Naziv=naziv;
        try
        {
            Context.Prodavnice!.Add(pp);
            await Context.SaveChangesAsync();
            return Ok(pp);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajAutomobil/{marka}/{boja}/{model}/{idProdavnice}")]
    public async Task<ActionResult> dodajAutomobil(string marka,string boja,string model, int idProdavnice)
    {
        if(string.IsNullOrWhiteSpace(marka))
            return BadRequest("Niste uneli marku!");
        
        //...

        Automobil a=new Automobil();
        a.Marka=marka;
        a.Boja=boja;
        a.Model=model;
        a.Kolicina=100;
        a.DatumPoslednjeProdaje="Nije kupljen nijedan ovakav model date marke!";
        a.Prodavnica= await Context.Prodavnice!.FindAsync(idProdavnice);
        //a.Prodavnica=Context.Prodavnice!.Where(p=>p.ID==idProdavnice).FirstOrDefault();
        //isto radi kao i ovo gore, samo drugacije zapisano
        try
        {
            Context.Automobili!.Add(a);
            await Context.SaveChangesAsync();
            return Ok(a);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("VratiSveProdavnice")]
    public async Task<ActionResult> vratiSveProdavnice()
    {
        var prodavnice=await Context.Prodavnice!
            .Include(q=>q.Automobili)
            .Select(p=>new{
                id=p.ID,
                naziv=p.Naziv,
                marke=p.Automobili!.Select(b=>b.Marka),
                modeli=p.Automobili!.Select(v=>v.Model),
                boje=p.Automobili!.Select(f=>f.Boja)
            }).ToListAsync();
            return Ok(prodavnice);
    }

    [HttpGet("VratiSveModele/{marka}/{idProdavnice}")]
    public async Task<ActionResult> vratiSveModele(string marka, int idProdavnice)
    {
        var modeli=await Context.Prodavnice!
            .Include(q=>q.Automobili)
            .Where(w=>w.ID==idProdavnice)
            .Select(p=>new{
                modeli=p.Automobili!
                .Where(r=>r.Marka==marka)
                .Select(g=>g.Model)
            }).ToListAsync();
        return Ok(modeli);
    }

    [HttpGet("VratiSveBoje/{marka}/{model}/{idProdavnice}")]
    public async Task<ActionResult> vratiSveBoje(string marka, string model, int idProdavnice)
    {
        var boje=await Context.Prodavnice!
            .Include(q=>q.Automobili)
            .Where(w=>w.ID==idProdavnice)
            .Select(p=>new{
                boje=p.Automobili!
                .Where(r=>r.Marka==marka&&r.Model==model)
                .Select(g=>g.Boja)
            }).ToListAsync();
        return Ok(boje);
    }

    [HttpPut("KupiAutomobil/{idAutomobila}")]
    public async Task<ActionResult> kupiAutomobil(int idAutomobila)
    {
        var auto=await Context.Automobili!.FindAsync(idAutomobila);
        if(auto==null)
        {
            return BadRequest("Nepostojeci auto!");
        }
        auto.Kolicina--;
        auto.DatumPoslednjeProdaje=DateTime.Now.ToShortDateString();
    
        try
        {
            Context.Automobili.Update(auto);
            await Context.SaveChangesAsync();
            return Ok(auto);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("ObrisiAutomobil/{idAutomobila}")]
    public async Task<ActionResult> obrisiAutomobil(int idAutomobila)
    {
        var a=await Context.Automobili!.FindAsync(idAutomobila);
        if (a != null)
            {
                Context.Automobili.Remove(a);
                await Context.SaveChangesAsync();
                return Ok($"ID obrisanog automobila je: {idAutomobila}");
            }
            else
            {
                return BadRequest($"Nije pronađen automobil sa ID: {idAutomobila}");
            }
    }
}
