namespace Models;

public class MeteoroloskiPodaci
{
    [Key]
    public int ID { get; set; }
    public string? NazivMeseca { get; set; }
    public int Temperatura { get; set; }
    public int KolicinaPadavina { get; set; }
    public int SuncaniDani { get; set; }
    [JsonIgnore]
    public Grad? Grad { get; set; }
}