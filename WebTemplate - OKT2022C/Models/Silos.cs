namespace Models;

public class Silos
{
    [Key]
    public int ID { get; set; }
    public string? Oznaka { get; set; }
    public int Kapacitet { get; set; }
    public int TrenutnaPopunjenost { get; set; }
    [Range(0,100)]
    public int Vlaznost { get; set; }
    public List<Praznjenje>? IsprazniliSuMe { get; set; }
}