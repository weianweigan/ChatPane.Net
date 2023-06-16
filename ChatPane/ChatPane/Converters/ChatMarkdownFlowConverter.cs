using System;
using System.Globalization;
using System.Windows.Data;
using ChatPane.Models;
using Markdig;

namespace ChatPane.Converters;

internal class ChatMarkdownFlowConverter : IMultiValueConverter
{
    public ChatMarkdownFlowConverter()
    {
        this._userConverter = new StringToMarkdownFlowDocumentConverter(MarkdownExtensions.UseCustomContainers(MarkdownExtensions.DisableHtml(new MarkdownPipelineBuilder())).Build());
    }

    public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null)
        {
            return null;
        }
        if (values.Length == 2 && values[1] is AIChatMessage)
        {
            return this._aiConverter.Convert(values[0], targetType, parameter, culture);
        }

        return this._userConverter.Convert(values[0], targetType, parameter, culture);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private readonly StringToMarkdownFlowDocumentConverter _userConverter;

    private readonly StringToMarkdownFlowDocumentConverter _aiConverter = new StringToMarkdownFlowDocumentConverter();
}
