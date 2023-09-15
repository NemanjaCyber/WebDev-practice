namespace Models;

public class Spoj
{
    public int ID { get; set; }
    [JsonIgnore]
    public Sastojak? Sastojak { get; set; }
    [JsonIgnore]
    public Sto? Sto { get; set; }
}