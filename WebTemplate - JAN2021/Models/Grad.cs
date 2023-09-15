namespace Models;

public class Grad
{
    [Key]
    public int ID { get; set; }
    public string? Ime { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public List<MeteoroloskiPodaci>? MeteoroloskiPodaci { get; set; }
}