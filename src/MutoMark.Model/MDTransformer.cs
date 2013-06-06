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
        private Markdown _processor = new Markdown();

        public string Transform(string markDown)
        {
            return this._processor.Transform(markDown);
        }
    }
}
