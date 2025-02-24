using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hibavonal.DataContext.Entities;

public class ErrorLog
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime ReportTime { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Comment { get; set; }

    [Required]
    public ErrorStatus Status { get; set; }

    [Required]
    public ErrorLevel Level { get; set; }

    public int RoomId { get; set; }

    [Required, ForeignKey("RoomId")]
    public Room Room { get; set; }

    public int MaintenanceWorkerId { get; set; }

    [ForeignKey("MaintenanceWorkerId")]
    public User MaintenanceWorker { get; set; }

    public int ReporterId { get; set; }

    [Required, ForeignKey("ReporterId")]
    public User Reporter { get; set; }
}
