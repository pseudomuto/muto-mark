using MarkdownSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutoMark.Model
{
    public class DefaultProcessor : IMarkdownProcessor
    {
        public string StylesheetName { get { return "default.style"; } }

        public string TemplateName { get { return "default.template"; } }

        public IMarkdownOptions CreateMarkdownOptions()
        {
            return new MarkdownOptions();
        }

        public void PreProcess(ref string markDown)
        {
            
        }

        public void PostProcess(ref string html)
        {
        }
    }
}
