namespace ChatPane.Models;

public class SystemChatMessage : ChatMessage
{
    public SystemChatMessage(string? content) : base(content)
    {
    }

    public override Role Role => Role.System;
}
