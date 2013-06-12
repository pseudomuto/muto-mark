using MarkdownSharp;
using System;
namespace MutoMark.Model
{
    public interface IMarkdownProcessor
    {
        string StylesheetName { get; }
        string TemplateName { get; }

        IMarkdownOptions CreateMarkdownOptions();
        void PreProcess(ref string markDown);
        void PostProcess(ref string html);
    }
}
