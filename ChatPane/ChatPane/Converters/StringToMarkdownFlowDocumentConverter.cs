using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using ChatPane.Models;
using Markdig;
using Markdig.Renderers;

namespace ChatPane.Converters;

internal class StringToMarkdownFlowDocumentConverter : IValueConverter
{
    private readonly MarkdownPipeline _pipeline;
    private readonly WpfRenderer _renderer;

    public StringToMarkdownFlowDocumentConverter()
    {
        this._pipeline = MarkdownExtensions.UseCustomContainers(new MarkdownPipelineBuilder()).Build();
        this._renderer = new WpfRenderer();
        this._pipeline.Setup(this._renderer);
    }

    public StringToMarkdownFlowDocumentConverter(MarkdownPipeline pipeline)
    {
        this._pipeline = pipeline;
        this._renderer = new WpfRenderer();
        pipeline.Setup(this._renderer);
    }

    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        FlowDocument? result;
        try
        {
            string? data = value?.ToString();
            if (string.IsNullOrEmpty(data))
            {
                result = new FlowDocument();
            }
            else 
            {
                data = data.Replace("\\r\\n", "&#x0a;");
                data = data.Replace("\\n\\r", "&#x0a;");
                FlowDocument flowDocument = Markdig.Wpf.Markdown.ToFlowDocument(data, this._pipeline, this._renderer);
                foreach (List list in flowDocument.Blocks.OfType<List>())
                {
                    if (list.ListItems.Any((ListItem i) => i.Blocks.OfType<List>().Any<List>()))
                    {
                        foreach (IEnumerable<Paragraph> enumerable in from i in list.ListItems
                                                                      select i.Blocks.OfType<Paragraph>())
                        {
                            foreach (Paragraph paragraph in enumerable)
                            {
                                paragraph.Margin = new Thickness(0.0, 10.0, 0.0, 10.0);
                            }
                        }
                    }
                }
                if (flowDocument != null)
                {
                    flowDocument.PagePadding = new Thickness(0.0);
                    flowDocument.Focusable = false;
                }
                result = flowDocument;
            }
        }
        catch (Exception)
        {
            //ActivityLog.LogError("StringToMarkdownFlowDocumentConverter", string.Format("Exception caught while converting to FlowDocument: {0}", arg));
            result = new FlowDocument();
        }
        return result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
