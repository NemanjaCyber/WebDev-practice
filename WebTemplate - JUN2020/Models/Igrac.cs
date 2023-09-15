namespace Models;

public class Igrac{
    [Key]
    public int ID { get; set; }
    public string? Ime { get; set; }
    public int Godine { get; set; }
    public int Rank { get; set; }
    [JsonIgnore]
    public Mec? MecNaKojemUcestvuje { get; set; }
}