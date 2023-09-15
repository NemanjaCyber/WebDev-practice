namespace Models;

public class Fabrika
{
    [Key]
    public int ID { get; set; }
    public string? Naziv { get; set; }
    public List<Silos>? Silosi { get; set; }
}