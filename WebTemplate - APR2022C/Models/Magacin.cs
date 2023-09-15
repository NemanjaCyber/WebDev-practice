namespace Models;

public class Magacin
{
    public int ID { get; set; }
    public string? Sara { get; set; }
    public float Duzina { get; set; }
    public float Sirina { get; set; }
    public int Cena { get; set; }
    [JsonIgnore]
    public Iverica? Iverica { get; set; }
}