namespace ChatPane.Models;

public class AIChatMessage : ChatMessage
{
    public AIChatMessage(string? content) : base(content)
    {
    }

    public override Role Role => Role.AI;
}