namespace Models;

public class Ptica
{
    [Key]
    public int ID { get; set; }
    public string? Naziv { get; set; }
    public string? Slika { get; set; }
    public List<Osobina>? Osobine { get; set; }
    public List<Vidjenje>? Vidjenja { get; set; }
}