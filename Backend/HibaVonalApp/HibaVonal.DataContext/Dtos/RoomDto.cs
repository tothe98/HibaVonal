using Hibavonal.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace HibaVonal.DataContext.Dtos
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "RoomType")]
    [JsonDerivedType(typeof(PersonalRoomDto), typeDiscriminator: "PersonalRoom")]
    [JsonDerivedType(typeof(SharedRoomDto), typeDiscriminator: "SharedRoom")]
    public abstract class RoomDto
    {
        [Required]
        public int Floor { get; set; }

        [Required]
        public int DormitoryId { get; set; }
        [ForeignKey("DormitoryId")]
        public Dormitory Dormitory { get; set; }

        public IList<Equipment>? Equipments { get; set; } = new List<Equipment>();
    }
}
