namespace ChatPane.Models;

public class ErrorChatMessage : ChatMessage
{
    public ErrorChatMessage(string? content) : base(content)
    {
    }

    public override Role Role => Role.Error;
}