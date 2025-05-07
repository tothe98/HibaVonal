namespace HibaVonal.Services.Exceptions;

public class RoomEquipmentAlreadyExistsException : Exception
{
    public RoomEquipmentAlreadyExistsException() : base("RoomEquipment already exists")
    {
    }
}
