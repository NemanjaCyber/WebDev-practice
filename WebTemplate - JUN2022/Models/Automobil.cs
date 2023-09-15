namespace Models;

public class Automobil
{
    [Key]
    public int ID { get; set; }
    public string? Marka { get; set; }
    public string? Boja { get; set; }
    public string? Model { get; set; }
    public string? DatumPoslednjeProdaje { get; set; }
    public int Kolicina { get; set; }
    public int Cena { get; set; }
    [JsonIgnore]
    public Prodavnica? Prodavnica { get; set; }
}