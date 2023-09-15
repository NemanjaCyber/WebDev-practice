namespace Models;

public class Iverica
{
    public int ID { get; set; }
    public required string Sara { get; set; }="";
    public float Duzina { get; set; }//2m
    public float Sirina { get; set; }//1m
    public int Kolicina { get; set; }//1 kolicina = 1 ploca 1m x 2m
    public int Cena { get; set; }//cena za 1m^
    public Prodavnica? Prodavnica { get; set; }
    public List<Magacin>? Otpadci { get; set; }
    public List<NarucenePloce>? NarucenePloce { get; set; }
}