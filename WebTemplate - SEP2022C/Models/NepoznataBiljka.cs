namespace Models;

public class NepoznataBiljka
{
    [Key]
    public int ID { get; set; }
    public string? Podrucje { get; set; }
    public string? Cvet { get; set; }
    public string? List { get; set; }
    public string? Stablo { get; set; }
    [JsonIgnore]
    public Sajt? Sajt { get; set; }
}