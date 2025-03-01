namespace HibaVonal.Services.Exceptions
{
    public class InvalidPropertyValueException : Exception
    {
        public InvalidPropertyValueException(string name, string type) : base($"A(z) {name} mező értéke nem felel meg a {type} típusnak!")
        {

        }
    }
}
