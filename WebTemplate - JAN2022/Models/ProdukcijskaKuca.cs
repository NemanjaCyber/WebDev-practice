namespace Models;

public class ProdukcijskaKuca
{
    [Key]
    public int ID { get; set; }
    public string? Naziv { get; set; }  
    public List<Film>? Filmovi { get; set; }
}