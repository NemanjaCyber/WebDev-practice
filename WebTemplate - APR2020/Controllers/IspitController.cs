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

    [HttpGet("PreuzmiKlubove")]
    public async Task<ActionResult> PreuzmiKlubove()
    {
        try
        {
            var klubovi=await Context.Klubovi!
            .Include(p=>p.Police)
            .ToListAsync();

            return Ok(klubovi);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
       
    }

    [HttpPut("AzurirajPolicu/{idPolice}/{brojDVDova}")]
    public async Task<ActionResult> AzurirajPolicu(int idPolice,int brojDVDova)
    {
        try
        {
            var polica=await Context.Police!
                .Where(p=>p.ID==idPolice).FirstOrDefaultAsync();

            if(polica!=null)
            {
                if(polica.TrenutnoDVD+brojDVDova>polica.MaxDVD)
                {
                    return BadRequest("Nema mesta na polici za tu kolicinu!");
                }
                else
                {
                    polica.TrenutnoDVD+=brojDVDova;
                    Context.Police!.Update(polica);
                    await Context.SaveChangesAsync();
                    return Ok($"Azurirana je polica sa ID: {polica.ID}");
                }
            }
            else
            {
                return BadRequest("Polica ne postoji!");
            }

        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
