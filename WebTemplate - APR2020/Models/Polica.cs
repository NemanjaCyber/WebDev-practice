namespace Models;

public class Polica
{
    [Key]
    public int ID { get; set; }
    public string? Oznaka { get; set; }
    public int MaxDVD { get; set; }
    public int TrenutnoDVD { get; set; }
    [JsonIgnore]
    public VideoKlub? MojKlub { get; set; }
}