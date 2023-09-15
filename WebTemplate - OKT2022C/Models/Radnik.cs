namespace Models;

public class Radnik
{
    [Key]
    public int ID { get; set; }
    public string? ImePrezime { get; set; }
    public List<Praznjenje>? IspraznjeniSilosi { get; set; }
}