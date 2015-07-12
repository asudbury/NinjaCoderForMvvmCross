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

            Assert.IsTrue(codeSnippet.Interfaces.Count == 3);
            Assert.IsTrue(codeSnippet.Interfaces[0] == "ILocationService");
            Assert.IsTrue(codeSnippet.Interfaces[1] == "IDropboxService");
            Assert.IsTrue(codeSnippet.Interfaces[2] == "IUknownService");

            Assert.IsTrue(codeSnippet.References.Count == 3);
            Assert.IsTrue(codeSnippet.References[0] == "Ref1");
            Assert.IsTrue(codeSnippet.References[1] == "Ref2");
            Assert.IsTrue(codeSnippet.References[2] == "Ref3");

            Assert.IsTrue(codeSnippet.UsingStatements.Count == 3);
            Assert.IsTrue(codeSnippet.UsingStatements[0] == "aaa.bbb.ccc");
            Assert.IsTrue(codeSnippet.UsingStatements[1] == "bbb.bbb.ccc");
            Assert.IsTrue(codeSnippet.UsingStatements[2] == "ccc.bbb.ccc");

            Assert.IsTrue(codeSnippet.Variables.Count == 2);
            Assert.IsTrue(codeSnippet.Variables[0] == "Var1");
            Assert.IsTrue(codeSnippet.Variables[1] == "Var2");

            Assert.IsTrue(codeSnippet.MockVariables.Count == 2);
            Assert.IsTrue(codeSnippet.MockVariables[0] == "Mock1");
            Assert.IsTrue(codeSnippet.MockVariables[1] == "Mock2");

            Assert.IsTrue(codeSnippet.TestInitMethod == "TestMethod");
            Assert.IsTrue(codeSnippet.Project == "Adrian");
            Assert.IsTrue(codeSnippet.Class == "Class1");
            Assert.IsTrue(codeSnippet.Method == "Method3");
            Assert.IsTrue(codeSnippet.Code == "Code7");
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

        /// <summary>
        /// Tests the cleaned code.
        /// </summary>
        [Test]
        public void TestCleanedCode()
        {
            CodeSnippetTranslator translator = new CodeSnippetTranslator();

            const string Code = "   hello\r";

            ////string cleanedCode = translator.CleanedCode(Code);

           //// Assert.IsTrue(cleanedCode == "hello");
        }
    }
}
