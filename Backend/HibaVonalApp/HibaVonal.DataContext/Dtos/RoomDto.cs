namespace HibaVonal.DataContext.Dtos;

public class RoomDto
{
    public int Id { get; set; }

    public int Floor { get; set; }

    public string RoomType { get; set; } // To differentiate room types

    public List<EquipmentDto>? Equipments { get; set; }
}
