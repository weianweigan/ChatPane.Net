namespace ChatPane.Models;

public class UserChatMessage : ChatMessage
{
    public UserChatMessage(string? content) : base(content)
    {
    }

    public override Role Role => Role.User;
}