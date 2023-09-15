namespace Models;

public class Biljka
{
    [Key]
    public int ID { get; set; }
    public string? Naziv { get; set; }
    public int Vidjanja { get; set; }
    public string? Podrucje { get; set; }
    public string? Cvet { get; set; }
    public string? List { get; set; }
    public string? Stablo { get; set; }
    public Sajt? Sajt { get; set; }
}