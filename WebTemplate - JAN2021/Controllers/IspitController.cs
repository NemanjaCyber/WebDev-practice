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

    // [HttpPost("DodajGrad")]
    // public async Task<ActionResult> DodajGrad([FromBody]Grad grad)
    // {
    //     var pomocni=Context.Gradovi!.Where(p=>p.Ime==grad.Ime).FirstOrDefault();
    //     if(pomocni!=null)
    //     {
    //         return BadRequest("Ovaj grad vec postoji!");
    //     }

    //     try
    //     {
    //         Context.Gradovi!.Add(grad);
    //         await Context.SaveChangesAsync();
    //         return Ok($"Dodat je grad sa ID: {grad.ID}");
    //     }
    //     catch(Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    [HttpGet("PreuzmiGradove")]
    public async Task<ActionResult> PreuzmiGradove()
    {
        try
        {
            var gradovi=await Context.Gradovi!
                .Include(p=>p.MeteoroloskiPodaci)
                .ToListAsync();
            return Ok(gradovi);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpPost("DodajMeteoroloskePodatke/{idGrada}/{nazivMeseca}/{prosecnaTemperatura}/{kolicinaPadavina}/{suncaniDani}")]
    public async Task<ActionResult> DodajMeteoroloskePodatke(int idGrada, string nazivMeseca,int prosecnaTemperatura, int kolicinaPadavina, int suncaniDani)
    {
        var meseci = new List<string>()
            {
                        "Januar",
                        "Februar",
                        "Mart",
                        "April",
                        "Maj",
                        "Jun",
                        "Jul",
                        "Avgust",
                        "Septembar",
                        "Oktobar",
                        "Novembar",
                        "Decembar"                    
            };
        
        if(!meseci.Contains(nazivMeseca))
        {
            return BadRequest("Nevalidan mesec!");
        }

        if(kolicinaPadavina<0)
        {
            return BadRequest("Nevalidna kolicina padavina!");
        }

        if(suncaniDani<0 || suncaniDani>31)
        {
            return BadRequest("Nevalidan broj suncanih dana!");
        }

        var mp=new MeteoroloskiPodaci();
        mp.Grad= await Context.Gradovi!.FindAsync(idGrada);
        mp.NazivMeseca=nazivMeseca;
        mp.KolicinaPadavina=kolicinaPadavina;
        mp.SuncaniDani=suncaniDani;
        mp.Temperatura=prosecnaTemperatura;

        try
        {
            Context.MeteoroloskiPodaci!.Add(mp);
            await Context.SaveChangesAsync();
            return Ok(mp);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("IzmeniTemperaturu/{idMeseca}/{novaTemperatura}")]
    public async Task<ActionResult> IzmeniTemperaturu(int idMeseca,int novaTemperatura)
    {
        var gradPomocni=Context.MeteoroloskiPodaci!.Where(p=>p.ID==idMeseca).FirstOrDefault();
        if(gradPomocni==null)
            return BadRequest("mesec ne postoji");
        
        gradPomocni.Temperatura=novaTemperatura;

        try
        {
            Context.MeteoroloskiPodaci!.Update(gradPomocni);
            await Context.SaveChangesAsync();
            return Ok(gradPomocni);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpPost("IzmeniPadavine/{idMeseca}/{novePadavine}")]
    public async Task<ActionResult> IzmeniPadavine(int idMeseca,int novePadavine)
    {
        var gradPomocni=Context.MeteoroloskiPodaci!.Where(p=>p.ID==idMeseca).FirstOrDefault();
        if(gradPomocni==null)
            return BadRequest("mesec ne postoji");
        
        gradPomocni.KolicinaPadavina=novePadavine;

        try
        {
            Context.MeteoroloskiPodaci!.Update(gradPomocni);
            await Context.SaveChangesAsync();
            return Ok(gradPomocni);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

}
