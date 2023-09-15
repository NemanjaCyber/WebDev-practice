namespace Models;

public class Film
{
    [Key]
    public int ID { get; set; }
    public string? Naziv { get; set; }
    public string? Kategorija { get; set; }
    public int ZbirOcena { get; set; }
    public int BrojOcena { get; set; }
    [JsonIgnore]
    public ProdKuca? Produkcija { get; set; }
}