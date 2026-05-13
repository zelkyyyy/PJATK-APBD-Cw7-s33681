namespace Cwiczenie7.DTOs;

public class ManufacturerDto
{
    public int Id { get; set; }
    public string Abbrevation { get; set; }
    public String FullName { get; set; } = null;
    public DateTime FoundationDate { get; set; }
}