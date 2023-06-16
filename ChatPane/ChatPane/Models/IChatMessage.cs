namespace ChatPane.Models;

public interface IChatMessage
{
    string? Content { get; set; }

    Role Role { get; }
}
