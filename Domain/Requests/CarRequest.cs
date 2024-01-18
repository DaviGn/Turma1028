namespace Domain.Requests;

public class BaseCarRequest
{
    public string? Brand { get; set; }
    public string? Model { get; set; }
}

public class UpdateCarRequest : BaseCarRequest
{
    public int Id { get; set; }
}