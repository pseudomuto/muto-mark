using MarkdownSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MutoMark.Model.Test.Processors
{
    public class GitHubProcessor
    {
        public class Integration
        {
            private string _subject = new Document(
                    ResourceLoader.GetResourceString("Resources.github.md"),
                    new Model.GitHubProcessor()
                ).ToString();

            [Fact]
            public void EscapesUnderscoresInWords()
            {
                Assert.Contains(
                        "perform_complicated_task or do_this_and_do_that_and_another_thing", 
                        this._subject
                    );
            }

            [Fact]
            public void EscapesUnderscoresInPreTags()
            {
                Assert.Contains("def robot_invasion", this._subject);
            }

            [Fact]
            public void NewLinesAreTreatedAsLineBreaks()
            {
                Assert.Contains("<p>Roses are red<br />\nViolets are blue</p>", this._subject);
            }

            [Fact]
            public void NewLinesWithTwoSpacesAreNewParagraphs()
            {
                Assert.Contains(
                    "<p>Roses are red</p>\n\n<p>\nViolets are blue</p>",
                    this._subject
                );
            }

            [Fact]
            public void CommitHashesAreAutoLinked()
            {
                Assert.Contains(
                    "<a href=\"/commit/be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2\" class=\"commit-link\"><tt>be6a8cc</tt></a>",
                    this._subject
                );
            }

            [Fact]
            public void CommitHashesAreAutoLinkedWithUser()
            {
                Assert.Contains(
                    "<a href=\"/commit/be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2\" class=\"commit-link\">mojombo@<tt>be6a8cc</tt></a>",
                    this._subject
                );
            }

            [Fact]
            public void CommitHashesAreAutoLinkedWithUserAndProject()
            {
                Assert.Contains(
                    "<a href=\"/commit/be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2\" class=\"commit-link\">mojombo/god@<tt>be6a8cc</tt></a>",
                    this._subject
                );
            }

            [Fact]
            public void IssuesAreAutoLinked()
            {
                Assert.Contains(
                        "<a href=\"/issues/1\" class=\"issue-link\">#1</a>", 
                        this._subject
                    );
            }

            [Fact]
            public void IssuesAreAutoLinkedWithUser()
            {
                Assert.Contains(
                        "<a href=\"/issues/1\" class=\"issue-link\">mojombo#1</a>",
                        this._subject
                    );
            }

            [Fact]
            public void IssuesAreAutoLinkedWithUserAndProject()
            {
                Assert.Contains(
                        "<a href=\"/issues/1\" class=\"issue-link\">mojombo/god#1</a>",
                        this._subject
                    );
            }
        }

        public abstract class GHTest
        {
            protected Model.GitHubProcessor _subject = new Model.GitHubProcessor();
        }

        public class Constructor : GHTest
        {
            [Fact]
            public void DefinesStylesheetName()
            {
                Assert.Equal("github.style", this._subject.StylesheetName);
            }

            [Fact]
            public void DefinesTemplateName()
            {
                Assert.Equal("github.template", this._subject.TemplateName);
            }
        }

        public class CreateMarkdowOptions
        {
            private IMarkdownOptions _subject = new Model.GitHubProcessor().CreateMarkdownOptions();

            [Fact]
            public void SetsAutoHyperlinkToTrue()
            {
                Assert.True(this._subject.AutoHyperlink);
            }

            [Fact]
            public void SetsAutoNewLinesToTrue()
            {
                Assert.True(this._subject.AutoNewLines);
            }

            [Fact]
            public void SetsLinkEmailsToTrue()
            {
                Assert.True(this._subject.LinkEmails);
            }
        }

        public class PreProcess : GHTest
        {
            protected void CompareResult(string expected, string source)
            {
                this._subject.PreProcess(ref source);
                Assert.Equal(expected, source);
            }

            public class Underscores : PreProcess
            {
                [Fact]
                public void AreEscapedWhenPartOfAWord()
                {
                    this.CompareResult(@"_em_ do\_this\_and\_that", "_em_ do_this_and_that");
                }
            }

            public class Emails : PreProcess
            {
                [Fact]
                public void AreSetupToBeLinks()
                {
                    this.CompareResult("<david.muto@gmail.com>", "david.muto@gmail.com");
                }
                
                [Fact]
                public void CheckForValidEmailBeforeLinking()
                {
                    this.CompareResult("david.muto@gmailcom", "david.muto@gmailcom");
                }
            }

            public class CommitHashes
            {                
                public class WhenIsolated : PreProcess
                {
                    [Fact]
                    public void AreConvertedToCommitLinks()
                    {
                        var expected = string.Format(
                                "<a href=\"{0}\" class=\"commit-link\"><tt>{1}</tt></a>",
                                "/commit/be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2",
                                "be6a8cc"
                            );

                        this.CompareResult(expected, "be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2");
                    }
                }

                public class WhenPrefixWithMention : PreProcess
                {
                    [Fact]
                    public void AreConvertedToUserSpecificCommitLinks()
                    {
                        var expected = string.Format(
                                "<a href=\"{0}\" class=\"commit-link\">{1}@<tt>{2}</tt></a>",
                                "/commit/be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2",
                                "mojombo",
                                "be6a8cc"
                            );

                        this.CompareResult(expected, 
                            "mojombo@be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2");
                    }
                }

                public class WhenPrefixWithMentionAndProject : PreProcess
                {
                    [Fact]
                    public void AreConvertedToUserAndProjectSpecificCommitLinks()
                    {
                        var expected = string.Format(
                                "<a href=\"{0}\" class=\"commit-link\">{1}@<tt>{2}</tt></a>",
                                "/commit/be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2",
                                "mojombo/god",
                                "be6a8cc"
                            );

                        this.CompareResult(expected,
                            "mojombo/god@be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2");
                    }
                }
            }

            public class IssueLinks
            {
                public class WhenIsolated : PreProcess
                {
                    [Fact]
                    public void AreConvertedToIssueLinks()
                    {
                        var expected = string.Format(
                                "<a href=\"{0}\" class=\"issue-link\">{1}</a>",
                                "/issues/1",
                                "#1"
                            );

                        this.CompareResult(expected, "#1");
                    }
                }

                public class WhenPrefixedWithMention : PreProcess
                {
                    [Fact]
                    public void AreConvertedToUserSpecificIssueLinks()
                    {
                        var expected = string.Format(
                                "<a href=\"{0}\" class=\"issue-link\">{1}</a>",
                                "/issues/1",
                                "mojombo#1"
                            );

                        this.CompareResult(expected, "mojombo#1");
                    }
                }

                public class WhenPrefixedWithMentionAndProject : PreProcess
                {
                    [Fact]
                    public void AreConvertedToUserAndProjectSpecificIssueLinks()
                    {
                        var expected = string.Format(
                                "<a href=\"{0}\" class=\"issue-link\">{1}</a>",
                                "/issues/1",
                                "mojombo/god#1"
                            );

                        this.CompareResult(expected, "mojombo/god#1");
                    }
                }
            }
        }
    }
}
