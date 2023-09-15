namespace Models;

public class Prodavnica
{
    [Key]
    public int ID { get; set; }
    public string? Naziv { get; set; }
    public int Zarada { get; set; }
    public List<Sto>? Stolovi { get; set; }
    public List<Sastojak>? Sastojci { get; set; }
}