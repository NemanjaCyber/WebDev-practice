namespace Models;

public class VideoKlub
{
    [Key]
    public int ID { get; set; }
    public string? Naziv { get; set; }
    public List<Polica>? Police { get; set; }   
}