namespace Models;

public class Mec{
    [Key]
    public int ID { get; set; }
    public string? Lokacija { get; set; }
    public string? Datum { get; set; }
    //broj poena u prvom setu(0 ili 1)
    public int S1 { get; set; }
    //broj poena u drugom setu(0 ili 1)
    public int S2 { get; set; }
    //broj poena prvog igraca u prvom setu
    public int PS11 { get; set; }
    //broj poena drugog igraca u prvom setu
    public int PS12 { get; set; }
    //broj poena prvog igraca u drugom setu
    public int PS21 { get; set; }
    //broj poena drugog igraca u drugom setu
    public int PS22 { get; set; }
    public List<Igrac>? Igraci { get; set; }
}