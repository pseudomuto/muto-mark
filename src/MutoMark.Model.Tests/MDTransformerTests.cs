using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestHelpers;

namespace MutoMark.Model.Tests
{
    [TestClass]
    public class MDTransformerTests
    {
        private MDTransformer _subject = new MDTransformer();

        [TestMethod]
        public void MDTransformer_TransformsMDToHTML()
        {
            var res = ResourceLoader.GetResourceString("Resources.README.md");
            var result = this._subject.Transform(res);

            StringAssert.Contains(result, "<h1>This is my title</h1>");
            StringAssert.Contains(result, "<p>A paragraph here</p>");
        }
    }
}
