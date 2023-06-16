using System;
using System.Windows;
using System.Windows.Controls;

namespace ChatPane;

/// <summary>
/// WPFChatPane.xaml 的交互逻辑
/// </summary>
public partial class WPFChatPane : UserControl
{
    public WPFChatPane()
    {
        InitializeComponent();
    }

    public DataTemplateSelector MessageDataTemplateSelector
    {
        get { return (DataTemplateSelector)GetValue(MessageDataTemplateSelectorProperty); }
        set { SetValue(MessageDataTemplateSelectorProperty, value); }
    }

    public static readonly DependencyProperty MessageDataTemplateSelectorProperty =
        DependencyProperty.Register("MessageDataTemplateSelector", typeof(DataTemplateSelector), typeof(WPFChatPane), new PropertyMetadata(null, MessageDataTemplateSelectorChanged));

    private static void MessageDataTemplateSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is WPFChatPane wpfChatPane && e.OldValue != e.NewValue)
        {
            wpfChatPane.MainItemsControl.ItemTemplateSelector = e.NewValue as DataTemplateSelector;
        }
    }
}
