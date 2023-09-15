namespace Models;

public class Sto
{
    [Key]
    public int ID { get; set; }
    public string? Oznaka { get; set; }
    public int KolicinaSastojaka { get; set; }
    public int VrednostStola { get; set; }
    [JsonIgnore]
    public Prodavnica? MojaProdavnica { get; set; }
    public List<Spoj>? SastojciNaStolu { get; set; }
}