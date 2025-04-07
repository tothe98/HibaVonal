namespace HibaVonal.Services.Exceptions;

public class AddressAlreadyExistsException : Exception
{
    public AddressAlreadyExistsException() : base("Address already exists")
    {
    }
}
