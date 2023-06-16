using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System;

namespace ChatPane.Models;

public abstract partial class ChatMessage : ObservableObject, IChatMessage
{
    private string? _content;

    protected ChatMessage(string? content)
    {
        Content = content;
    }

    public string? Content
    {
        get => _content; set
        {
            SetProperty(ref _content ,value);
        }
    }

    public abstract Role Role { get; }

    [RelayCommand]
    private void Copy()
    {
        try
        {
            Clipboard.SetDataObject(Content);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    public override string ToString() => Role.ToString();
}