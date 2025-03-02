using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hibavonal.DataContext.Entities;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "RoomType")]
[JsonDerivedType(typeof(PersonalRoom), typeDiscriminator: "PersonalRoom")]
[JsonDerivedType(typeof(SharedRoom), typeDiscriminator: "SharedRoom")]
public abstract class Room
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int Floor { get; set; }

    [Required]
    public int DormitoryId { get; set; }

    [ForeignKey("DormitoryId")]
    public Dormitory? Dormitory { get; set; }
    public IList<Equipment>? Equipments { get; set; } = new List<Equipment>();
}
