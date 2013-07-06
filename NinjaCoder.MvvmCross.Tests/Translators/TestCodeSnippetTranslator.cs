// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestCodeSnippetTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Translators
{
    using NinjaCoder.MvvmCross.Translators;
    using NUnit.Framework;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    /// Defines the TestCodeSnippetTranslator type.
    /// </summary>
    [TestFixture]
    public class TestCodeSnippetTranslator
    {
        /// <summary>
        /// Tests the translator.
        /// </summary>
        [Test]
        public void TestTranslator()
        {
            CodeSnippetTranslator translator = new CodeSnippetTranslator();

            CodeSnippet codeSnippet = translator.Translate(Helper.GetTestDataPath("CodeSnippet.xml"));

            Assert.IsTrue(codeSnippet.Interfaces.Count > 0);
            Assert.IsTrue(codeSnippet.References.Count > 0);
        }
        
        /// <summary>
        /// Tests the translator file does not exist.
        /// </summary>
        [Test]
        public void TestTranslatorFileDoesNotExist()
        {
            CodeSnippetTranslator translator = new CodeSnippetTranslator();

            const string FileName = @"C";

            CodeSnippet codeSnippet = translator.Translate(FileName);

            Assert.IsTrue(codeSnippet == null);
        }
    }
}
