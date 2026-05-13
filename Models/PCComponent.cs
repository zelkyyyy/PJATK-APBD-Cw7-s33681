using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenie7.Models;

[PrimaryKey(nameof(PCId), nameof(ComponentCode))]
public class PCComponent
{
    public int PCId { get; set; }

    [Column(TypeName = "char(10)")]
    [MaxLength(10)]
    public string ComponentCode { get; set; } = null;

    public int Amount { get; set; }

    [ForeignKey(nameof(PCId))] 
    public PC PC { get; set; } = null;
    
    [ForeignKey(nameof(ComponentCode))]
    public Component Component { get; set; } = null;
}