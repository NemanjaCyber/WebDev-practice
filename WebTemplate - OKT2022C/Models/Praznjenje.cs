namespace Models;

public class Praznjenje
{
    [Key]
    public int ID { get; set; }
    public int IspraznjenaKolicina { get; set; }
    [JsonIgnore]
    public Radnik? Radnik { get; set; }
    [JsonIgnore]
    public Silos? Silos { get; set; }
}