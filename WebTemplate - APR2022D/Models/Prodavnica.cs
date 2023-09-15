namespace Models;

public class Prodavnica
{
    [Key]
    public int ID { get; set; }
    public required string Naziv { get; set; }
    public List<Komponenta>? Komponente { get; set; }
}