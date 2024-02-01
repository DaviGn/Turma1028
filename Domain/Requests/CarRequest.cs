namespace Domain.Requests;

public class BaseCarRequest
{
    public int UserId { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
}

public class UpdateCarRequest : BaseCarRequest
{
    public int Id { get; set; }
}