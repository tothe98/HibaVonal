using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.DataContext.Dtos
{
    public class ErrorLogDto
    {
        public int Id { get; set; }
        public DateTime ReportTime { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string Level { get; set; }
        public RoomDto? Room { get; set; }
        public UserDataDto? MaintenanceWorker { get; set; }
        public UserDataDto? Reporter { get; set; }
    }

    public class ErrorLogCreateDto
    {
        public string Description { get; set; }
        public EErrorLevel Level { get; set; }
        public int RoomId { get; set; }
    }

    public class ErrorLogUpdateDto
    {
        public string? Description { get; set; }
        public string? Comment { get; set; }
        public EErrorStatus? Status { get; set; }
        public EErrorLevel? Level { get; set; }
        public int? RoomId { get; set; }
        public int? MaintenanceWorkerId { get; set; }
        public int? ReporterId { get; set; }
    }

    public class ErrorLogReporterUpdateDto
    {
        public string Description { get; set; }
        public EErrorLevel Level { get; set; }
    }
}
