using MarkdownSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutoMark.Model
{
    public class MDTransformer
    {
        public IMarkdownProcessor Processor { get; private set; }

        public MDTransformer(IMarkdownProcessor processor)
        {
            this.Processor = processor;
        }

        public string Transform(string markDown)
        {
            this.Processor.PreProcess(ref markDown);

            var md = new Markdown(this.Processor.CreateMarkdownOptions());
            var result = md.Transform(markDown);
            this.Processor.PostProcess(ref result);

            return result;
        }
    }
}
