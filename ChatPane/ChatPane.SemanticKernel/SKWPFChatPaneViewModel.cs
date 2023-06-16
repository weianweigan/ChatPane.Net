using ChatPane.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SemanticFunctions;
using Microsoft.SemanticKernel.SkillDefinition;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChatPane.SemanticKernel;

public class SKWPFChatPaneViewModel : WPFChatPaneViewModel
{
    private const string DEFAULT_PROMPT =
        """
        {{$input}}
        """;

    public SKWPFChatPaneViewModel(
        IKernel kernel, 
        string? defaultPrompt = null,
        PromptTemplateConfig? promptTemplateConfig = null)
    {
        Kernel = kernel;
        var sKFunctionString = string.IsNullOrEmpty(defaultPrompt) ? DEFAULT_PROMPT : defaultPrompt;
        CurrentSKFunction = Kernel.CreateSemanticFunction(sKFunctionString);
        SKFunctions = new List<ISKFunction>() { CurrentSKFunction };
        PromptTemplateConfig = promptTemplateConfig;
    }

    public SKWPFChatPaneViewModel(
        IKernel kernel,
        IList<ISKFunction> functions)
    {
        Kernel = kernel;
        SKFunctions = functions;
        CurrentSKFunction = SKFunctions.FirstOrDefault();
    }

    public IKernel Kernel { get; }

    public PromptTemplateConfig? PromptTemplateConfig { get; }

    public ISKFunction? CurrentSKFunction { get; set; }

    public IList<ISKFunction> SKFunctions { get; }

    protected override async Task SendAsync(CancellationToken cancellationToken)
    {
        string input = Input;
        Input = string.Empty;
        Messages.Add(new UserChatMessage(input));

        if (CurrentSKFunction == null)
        {
            Messages.Add(new ErrorChatMessage("No function selected."));
            return;
        }

        try
        {
            SKContext context = Kernel.CreateNewContext();
            context["input"] = input;
            SKContext result = await CurrentSKFunction.InvokeAsync(context);

            if (result.ErrorOccurred)
            {
                Messages.Add(new ErrorChatMessage(result.LastErrorDescription));
            }
            else
            {
                Messages.Add(new AIChatMessage(result.ToString()));
            }
        }
        catch (System.Exception ex)
        {
            Messages.Add(new AIChatMessage(ex.Message));
        }
    }
}