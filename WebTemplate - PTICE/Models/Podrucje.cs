namespace Models;

public class Podrucje
{
    [Key]
    public int ID { get; set; }
    public string? Naziv { get; set; }
    public List<Vidjenje>? Vidjenja { get; set; }
}