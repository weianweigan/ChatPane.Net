using ChatPane.Models;
using System.Windows;
using System.Windows.Controls;

namespace ChatPane;

public class MessageDataTemplateSelector : DataTemplateSelector
{
    public override DataTemplate? SelectTemplate(object item, DependencyObject container)
    {
        if(item == null) { return null; }

        return item switch
        {
            SystemChatMessage or AIChatMessage => AIMessageDataTemplate,
            UserChatMessage or ErrorChatMessage => UserMessageDataTemplate,
            _ => null
        };
    }

    public DataTemplate? UserMessageDataTemplate { get; set; }

    public DataTemplate? AIMessageDataTemplate { get; set;}
}
