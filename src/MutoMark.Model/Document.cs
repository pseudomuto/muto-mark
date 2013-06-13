using MarkdownSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MutoMark.Model
{
    public class Document
    {
        private MDTransformer _transformer;
        
        public string MarkDownSource { get; private set; }

        public string HTMLResult { get; private set; }

        public IMarkdownProcessor Processor { get { return this._transformer.Processor; } }

        public Document(string markDown, IMarkdownProcessor processor)
        {
            this.MarkDownSource = markDown;
            
            this._transformer = new MDTransformer(processor);
            this.HTMLResult = this._transformer.Transform(markDown);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("<html><head>");            
            this.AddStylesheet(sb);
            sb.Append("</head><body>");

            var template = this.GetTemplate(this.Processor.TemplateName);
            var html = template.Replace("##BODY##", this.HTMLResult);
                        
            return sb.Append(html).Append("</body></html>").Replace("\r", "\r\n").ToString();
        }

        private void AddStylesheet(StringBuilder sb)
        {
            var styleSheet = this.Processor.StylesheetName;

            if (!string.IsNullOrEmpty(styleSheet))
            {
                sb.Append("<style type=\"text/css\">");
                sb.Append(this.GetStyleSheet(styleSheet));
                sb.Append("</style>");
            }
        }

        private string GetTemplate(string name)
        {
            return this.GetResource(name + ".html");
        }

        private string GetStyleSheet(string name)
        {
            return this.GetResource(name + ".css");
        }

        private string GetResource(string name)
        {
            Stream stream = null;
            string result = string.Empty;

            try
            {
                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MutoMark.Model.Resources." + name);
                using (var reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }

            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }

            return result;
        }
    }
}
