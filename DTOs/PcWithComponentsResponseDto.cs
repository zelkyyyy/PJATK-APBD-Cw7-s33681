using Cwiczenie7.Models;

namespace Cwiczenie7.DTOs;

public class PcWithComponentsResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
    public List<PCComponentDetailDto> Components { get; set; } = new();
}