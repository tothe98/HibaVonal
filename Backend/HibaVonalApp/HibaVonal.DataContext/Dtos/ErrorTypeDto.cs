namespace HibaVonal.DataContext.Dtos;

public class ErrorTypeDto
{
    public int Id { get; set; }

    public string Name { get; set; }
}

public class ErrorTypeCreateUpdateDto
{
    public string Name { get; set; }
}
