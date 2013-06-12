using MarkdownSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MutoMark.Model
{
    public class GitHubProcessor : IMarkdownProcessor
    {
        public string StylesheetName { get { return "github.style"; } }

        public string TemplateName { get { return "github.template"; } }

        public IMarkdownOptions CreateMarkdownOptions()
        {
            var options = new MarkdownOptions();
            options.AutoHyperlink = true;
            options.AutoNewLines = true;
            options.LinkEmails = true;
            
            return options;
        }

        public void PreProcess(ref string markDown)
        {
            FixInnerUnderscores(ref markDown);
            FixEmails(ref markDown);
            FixCommitLinks(ref markDown);
            FixIssueLinks(ref markDown);
        }

        public void PostProcess(ref string html)
        {
            html = Regex.Replace(html, @"([^_])\\_([^_])", "$1_$2");
            html = Regex.Replace(html, @"\s{2,}<br />$", "</p>\n\n<p>", RegexOptions.Multiline);
        }

        private static void FixEmails(ref string markDown)
        {
            markDown = Regex.Replace(
                    markDown,
                    @"\b([-.\w]+\@[-a-z0-9]+(\.[-a-z0-9]+)*\.[a-z]+)\b",
                    "<$1>"
                );
        }

        private static void FixIssueLinks(ref string markDown)
        {
            markDown = Regex.Replace(
                    markDown,
                    @"\s([\w\/]+)?#(\d+)\b",
                    "<a href=\"/issues/$2\" class=\"issue-link\">$1#$2</a>"
                );            
        }

        private static void FixCommitLinks(ref string markDown)
        {
            markDown = Regex.Replace(
                    markDown, 
                    @"\s([\w\/]+\@)?([\w\d]{7})([\w\d]{33})\b", 
                    "<a href=\"/commit/$2$3\" class=\"commit-link\">$1<tt>$2</tt></a>"
                );
        }

        private static void FixInnerUnderscores(ref string markDown)
        {
            markDown = Regex.Replace(markDown, @"([^_])_([^_])", "$1\\_$2");
        }        
    }
}
