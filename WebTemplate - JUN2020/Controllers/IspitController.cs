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

    [HttpGet("VratiMeceve")]
    public async Task<ActionResult> VratiMeceve()
    {
        var mecevi= await Context.Mecevi!
            .Include(p=>p.Igraci).ToListAsync();
        
        return Ok(mecevi);
    }

    [HttpPut("DodajPoenPrviIgrac/{mecId}")]
    public async Task<ActionResult> DodajPoenPrviIgrac(int mecId)
    {
        var mec=Context.Mecevi!.Where(p=>p.ID==mecId).FirstOrDefault();
    
        if(mec==null){
            return BadRequest("Mec sa zadatim id ne postoji.");
        }

        if(mec.S1+mec.S2==2){
            return BadRequest("Kraj meca!");
        }

        if(mec.S1==0 && mec.S2==0)
        {
            mec.PS11++;
            if(mec.PS11==6){
                mec.S1++;
            }
        }
        else
        {
            mec.PS21++;
            if(mec.PS21==6){
                mec.S1++;
            }
        }
        Context.Mecevi!.Update(mec);
        await Context.SaveChangesAsync();
        return Ok(mec);
    }

    [HttpPut("DodajPoenDrugiIgrac/{mecId}")]
    public async Task<ActionResult> DodajPoenDrugiIgrac(int mecId)
    {
        var mec=Context.Mecevi!.Where(p=>p.ID==mecId).FirstOrDefault();
    
        if(mec==null){
            return BadRequest("Mec sa zadatim id ne postoji.");
        }

        if(mec.S1+mec.S2==2){
            return BadRequest("Kraj meca!");
        }

        if(mec.S2==0 && mec.S1==0)
        {
            mec.PS12++;
            if(mec.PS12==6){
                mec.S2++;
            }
        }
        else
        {
            mec.PS22++;
            if(mec.PS22==6){
                mec.S2++;
            }
        }
        Context.Mecevi!.Update(mec);
        await Context.SaveChangesAsync();
        return Ok(mec);
    }
}
