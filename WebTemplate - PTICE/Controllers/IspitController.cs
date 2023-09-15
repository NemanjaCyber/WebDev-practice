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

    [HttpGet("PreuzmiPodrucja")]
    public async Task<ActionResult> PreuzmiPodrucja()
    {
        var podaci=await Context.Podrucja!
            .Include(p=>p.Vidjenja)
            .Select(q=>new{
                PodrucjeID=q.ID,
                PodrucjeNaziv=q.Naziv,
                BrojVidjenja=q.Vidjenja!.Count()
            }).ToListAsync();

        return Ok(podaci);
    }

    [HttpGet("PreuzmiOsobine")]
    public async Task<ActionResult> PreuzmiOsobine()
    {
        var podaci=await Context.Osobine!
            .Select(q=>new{
                OsobinaID=q.ID,
                OsobinaNaziv=q.Naziv,
                OsobinaVrednosti=q.Vrednost
            }).ToListAsync();
        
        return Ok(podaci);
    }

    
}
