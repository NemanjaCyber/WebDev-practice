namespace Models;

public class Silos
{
    [Key]
    public int ID { get; set; }
    public string? Oznaka { get; set; }
    public int Kapacitet { get; set; }
    public int TrenutnaPopunjenost { get; set; }
    [JsonIgnore]
    public Fabrika? MojaFabrika { get; set; }
}