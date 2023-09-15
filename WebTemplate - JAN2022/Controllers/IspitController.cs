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

    [Route("VratiSve")]
    [HttpGet]
    public async Task<ActionResult> vratiSve()
    {
        var sve=await Context.ProdukcijskeKuce!
            .Include(p=>p.Filmovi)
            .ToListAsync();

        return Ok(sve);
    }


    [Route("VratiProdukcijeSaKategorijama")]
    [HttpGet]
    public async Task<ActionResult> VratiProdukcijeSaKategorijama()
    {
        var sve=await Context.ProdukcijskeKuce!
            .Include(p=>p.Filmovi)
            .Select(q=>new{
                id=q.ID,
                naziv=q.Naziv,
                kategorije=q.Filmovi!
                    .Select(r=>r.Kategorija)
            })
            .ToListAsync();
            return Ok(sve);
    }

    [Route("VratiFilmoveZaKateogrije/{kategorija}/{idProdukcije}")]
    [HttpGet]
    public async Task<ActionResult> VratiFilmoveZaKategorije(string kategorija, int idProdukcije)
    {
        var filmovi=await Context.ProdukcijskeKuce!
            .Include(q=>q.Filmovi!.Where(r=>r.Kategorija==kategorija))
            .Where(p=>p.ID==idProdukcije).ToListAsync();
        return Ok(filmovi); 
    }
    

    [Route("DodajOcenu/{idFilma}/{Ocena}")]
    [HttpPut]
    public async Task<ActionResult> DodajOCenu(int idFilma, int Ocena)
    {
        var f=await Context.Filmovi!.Where(p=>p.ID==idFilma).FirstOrDefaultAsync();
        f!.BrOcena++;
        f.UkupnaOcena+=Ocena;

        try
        {
            Context.Filmovi!.Update(f);
            await Context.SaveChangesAsync();
            return Ok(f);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
