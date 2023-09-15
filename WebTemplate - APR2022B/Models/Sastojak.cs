namespace Models;

public class Sastojak
{
    [Key]
    public int ID { get; set; }
    public string? Naziv { get; set; }
    public int Cena { get; set; }
    [JsonIgnore]
    public Prodavnica? Prodavnica { get; set; }
    public List<Spoj>? Spoj { get; set; }
}