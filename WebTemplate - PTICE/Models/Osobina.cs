namespace Models;

public class Osobina
{
    [Key]
    public int ID { get; set; }
    public string? Naziv { get; set; }
    public string? Vrednost { get; set; }
    public List<Ptica>? Ptica { get; set; }
    public List<NepoznataPtica>? Nepoznata { get; set; }
}