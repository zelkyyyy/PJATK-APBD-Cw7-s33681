namespace Cwiczenie7.DTOs;

public class ComponentDetailDto
{
    public String Code { get; set; } = null;
    public String Name { get; set; } = null;
    public String? Description { get; set; }
    public ManufacturerDto Manufacturer { get; set; } = null!;
    public ComponentTypeDto Type { get; set; } = null!;
}