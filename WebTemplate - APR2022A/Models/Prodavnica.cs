namespace Models;

public class Prodavnica
{
    [Key]
    public int ID { get; set; }
    public string? Naziv { get; set; }
    public List<Artikl>? Artikli { get; set; }
}