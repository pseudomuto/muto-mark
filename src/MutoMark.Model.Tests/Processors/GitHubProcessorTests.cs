using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MutoMark.Model.Tests.Processors
{
    [TestClass]
    public class GitHubProcessorTests
    {
        private Document _subject = new Document(
                ResourceLoader.GetResourceString("Resources.github.md"), 
                new GitHubProcessor()
            );

        [TestMethod]
        public void GitHubProcessor_SpecifiesStylesheet()
        {
            Assert.AreEqual("github.style", this._subject.Processor.StylesheetName);
        }

        [TestMethod]
        public void GitHubProcessor_SpecifiesTemplate()
        {
            Assert.AreEqual("github.template", this._subject.Processor.TemplateName);
        }

        #region [Literal Underscores]
        
        [TestMethod]
        public void GitHubProcessor_IgnoresUnderscoresInWords()
        {
            var expected = "perform_complicated_task or do_this_and_do_that_and_another_thing";
            StringAssert.Contains(this._subject.ToString(), expected);
        }

        [TestMethod]
        public void GitHubProcessor_IgnoresUnderscoresInPreTags()
        {
            var expected = "def robot_invasion";
            StringAssert.Contains(this._subject.ToString(), expected);
        }

        #endregion

        #region [New Lines]

        [TestMethod]
        public void GitHubProcessor_TreatsNewLinesAsLiterals()
        {
            var expected = "<p>Roses are red<br />\nViolets are blue</p>";
            StringAssert.Contains(this._subject.ToString(), expected);
        }

        [TestMethod]
        public void GitHubProcessor_IgnoresNewLineRuleWhenTwoSpacesExistBeforeLinebreak()
        {
            var expected = "<p>Roses are red</p>\n\n<p>\nViolets are blue</p>";
            StringAssert.Contains(this._subject.ToString(), expected);
        }

        #endregion

        #region [Commit Links]
        
        [TestMethod]
        public void GitHubProcessor_AutoLinksSHAHashes()
        {
            var expected = "<a href=\"/commit/be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2\" class=\"commit-link\"><tt>be6a8cc</tt></a>";
            StringAssert.Contains(this._subject.ToString(), expected);
        }

        [TestMethod]
        public void GitHubProcessor_AutoLinksUserSHAHashes()
        {
            var expected = "<a href=\"/commit/be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2\" class=\"commit-link\">mojombo@<tt>be6a8cc</tt></a>";
            StringAssert.Contains(this._subject.ToString(), expected);
        }

        [TestMethod]
        public void GitHubProcessor_AutoLinksUserProjectSHAHashes()
        {
            var expected = "<a href=\"/commit/be6a8cc1c1ecfe9489fb51e4869af15a13fc2cd2\" class=\"commit-link\">mojombo/god@<tt>be6a8cc</tt></a>";
            StringAssert.Contains(this._subject.ToString(), expected);
        }

        #endregion

        #region [Issue Links]
        
        [TestMethod]
        public void GitHubProcessor_AutoLinksIssues()
        {
            var expected = "<a href=\"/issues/1\" class=\"issue-link\">#1</a>";
            StringAssert.Contains(this._subject.ToString(), expected);
        }

        [TestMethod]
        public void GitHubProcessor_AutoLinksUserIssues()
        {
            var expected = "<a href=\"/issues/1\" class=\"issue-link\">mojombo#1</a>";
            StringAssert.Contains(this._subject.ToString(), expected);
        }

        [TestMethod]
        public void GitHubProcessor_AutoLinksUserProjectIssues()
        {
            var expected = "<a href=\"/issues/1\" class=\"issue-link\">mojombo/god#1</a>";
            StringAssert.Contains(this._subject.ToString(), expected);
        }

        #endregion        
    }
}
