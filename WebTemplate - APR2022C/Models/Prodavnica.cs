namespace Models;

public class Prodavnica
{
    public int ID { get; set; }
    public required string Naziv { get; set; }
    public int Zarada { get; set; }
    public List<Iverica>? Iverice { get; set; }
    
}