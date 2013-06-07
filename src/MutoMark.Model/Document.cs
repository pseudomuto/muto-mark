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
        private Markdown _processor = new Markdown();

        public string MarkDownSource { get; private set; }

        public Document(string markDown)
        {
            this.MarkDownSource = markDown;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("<style type=\"text/css\">");
            sb.Append(this.GetStyleSheet("github"));
            sb.Append("</style>");

            sb.Append("<div class=\"js-comment-body comment-body markdown-body markdown-format\">");
            var html = this._processor.Transform(this.MarkDownSource);
            return sb.Append(html).Append("</div>").ToString();
        }

        private string GetStyleSheet(string name)
        {
            Stream stream = null;
            string result = string.Empty;

            try
            {
                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MutoMark.Model.Resources." + name + ".css");
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
