using ChatPane.Models;
using System.Collections.ObjectModel;

namespace ChatPane;

public interface IChatPaneViewModel
{
    ObservableCollection<IChatMessage> Messages { get; }
}