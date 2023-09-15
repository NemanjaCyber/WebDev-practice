namespace Models;

public class Vidjenje
{
    [Key]
    public int ID { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Vreme { get; set; }
    public Ptica? Ptica { get; set; }
    public Podrucje? Podrucje { get; set; }
}