namespace Models;

public class Sajt
{
    [Key]
    public int ID { get; set; }
    public string? Naziv { get; set; }
    public List<Biljka>? Biljke { get; set; }
    public List<NepoznataBiljka>? NepoznateBiljke { get; set; }
}