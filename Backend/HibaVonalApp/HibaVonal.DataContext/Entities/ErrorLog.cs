using HibaVonal.DataContext.Entities;
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
    public string? Comment { get; set; }

    [Required]
    public EErrorStatus Status { get; set; }

    [Required]
    public EErrorLevel Level { get; set; }

    public int? RoomId { get; set; }
    [ForeignKey("RoomId")]
    public Room? Room { get; set; }

    public int? MaintenanceWorkerId { get; set; }
    [ForeignKey("MaintenanceWorkerId")]
    public User? MaintenanceWorker { get; set; }

    public int? ReporterId { get; set; }
    [ForeignKey("ReporterId")]
    public User? Reporter { get; set; }
}
