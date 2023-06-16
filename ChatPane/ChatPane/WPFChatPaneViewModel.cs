using ChatPane.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;

namespace ChatPane;

public abstract partial class WPFChatPaneViewModel : ObservableObject,IChatPaneViewModel
{
    private IAsyncRelayCommand? _sendCommand;
    private string _input = string.Empty;

    public WPFChatPaneViewModel()
    {
    }

    public string Input { get => _input; set => SetProperty(ref _input, value); }

    public ObservableCollection<IChatMessage> Messages { get; } = new();

    public IAsyncRelayCommand SendCommand { get => _sendCommand ??= new AsyncRelayCommand(SendAsync,CanSend); }

    protected virtual bool CanSend() => true;

    [RelayCommand(CanExecute = nameof(CanStop))]
    protected virtual void Stop()
    {
        SendCommand?.Cancel();
    }

    protected virtual bool CanStop() => true;

    protected abstract Task SendAsync(CancellationToken cancellationToken);

    [RelayCommand]
    private void RemoveChatData(ChatMessage chatMessage)
    {
        if (chatMessage != null)
        {
            Messages.Remove(chatMessage);
        }
    }
}
