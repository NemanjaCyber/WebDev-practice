namespace Models;

public class Komponenta
{
    [Key]
    public int ID { get; set; }
    public required string Naziv { get; set; }
    public int Sifra { get; set; }
    public int Kolicina { get; set; }
    public required string Brend { get; set; }
    public required string Tip { get; set; }
    public int Cena { get; set; }

}