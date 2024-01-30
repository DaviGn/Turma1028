namespace Domain.Options;

public class PasswordHashOptions
{
    public const string Section = "PasswordHash";

    public int MinIteration { get; set; }
    public int MaxIteration { get; set; }
    public int SaltByteSize { get; set; }
    public int HashByteSize { get; set; }
    public string HashAlgorithm { get; set; } = string.Empty;
}
