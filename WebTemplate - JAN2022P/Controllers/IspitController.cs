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

    [HttpGet("VratiProdukcijeSaKategorijama")]
    public async Task<ActionResult> VratiProdukcijeSaKategorijama()
    {
        var podaci=await Context.ProdukcijskeKuce!
            .Include(p=>p.Filmovi)
            .Select(q=>new{
                ProdukcijskaKucaID=q.ID,
                ProdukcijsaKucaNaziv=q.Naziv,
                Kategorije=q.Filmovi!
                    .Select(r=>r.Kategorija)
            }).ToListAsync();

        return Ok(podaci);
    }

    [HttpGet("VratiFilmoveZaKategorije/{kategorija}/{idProdukcije}")]
    public async Task<ActionResult> VratiFilmoveZaKategorije(string kategorija,int idProdukcije)
    {
        var filmovi=await Context.ProdukcijskeKuce!
            .Include(p=>p.Filmovi!.Where(r=>r.Kategorija==kategorija))
            .Where(o=>o.ID==idProdukcije)
            .ToListAsync();
    
        return Ok(filmovi);
    }

    [HttpPut("DodajOcenu/{idFilma}/{Ocena}")]
    public async Task<ActionResult> DodajOcenu(int idFilma, int Ocena)
    {
        if(Ocena<1 || Ocena>10) return BadRequest("Nevalidna Ocena.");

        var filmZaPromenu=await Context.Filmovi!.Where(p=>p.ID==idFilma).FirstOrDefaultAsync();
        filmZaPromenu!.ZbirOcena+=Ocena;
        filmZaPromenu!.BrojOcena++;

        try
        {
            Context.Filmovi!.Update(filmZaPromenu);
            await Context.SaveChangesAsync();
            return Ok("Uspesno dodata ocena.");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    
    }
}
