namespace Models;

public class Artikl
{
    [Key]
    public int ID  { get; set; }
    public string? Sifra { get; set; }
    public string? Brend { get; set; }
    public string? Velicina { get; set; }
    public int Kolicina { get; set; }
    public int Cena { get; set; }
    [JsonIgnore]
    public Prodavnica? Prodavnica { get; set; }
}