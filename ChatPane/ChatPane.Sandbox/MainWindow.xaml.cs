using ChatPane.SemanticKernel;
using Microsoft.SemanticKernel;
using System.Windows;

namespace ChatPane.Sandbox;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private class OpenAIConfig
    {
        public const string OpenAIKey = "Your OpenAI APIKey";

        public const string Model = "gpt-3.5-turbo";
    }

    public MainWindow()
    {
        InitializeComponent();

        var builder = new KernelBuilder();
        builder
            .WithOpenAIChatCompletionService(OpenAIConfig.Model, OpenAIConfig.OpenAIKey)
            .WithOpenAITextEmbeddingGenerationService(OpenAIConfig.Model, OpenAIConfig.OpenAIKey);

        IKernel kernel = builder.Build();

        DataContext = new SKWPFChatPaneViewModel(kernel);
    }

}
