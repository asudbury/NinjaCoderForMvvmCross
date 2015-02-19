// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestingConstants type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Constants
{
    /// <summary>
    /// Defines the TestingConstants type.
    /// </summary>
    public static class TestingConstants
    {
        /// <summary>
        /// Defines the NUnit type.
        /// </summary>
        public static class NUnit
        {
            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "NUnit";

            /// <summary>
            /// The class attribute.
            /// </summary>
            public const string ClassAttribute = "[TestFixture]";

            /// <summary>
            /// The method attribute.
            /// </summary>
            public const string MethodAttribute = "[Test]";
        }

        /// <summary>
        /// Defines the MsTest type.
        /// </summary>
        public static class MsTest
        {
            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "MSTest";

            /// <summary>
            /// The class attribute.
            /// </summary>
            public const string ClassAttribute = "[TestClass]";

            /// <summary>
            /// The method attribute.
            /// </summary>
            public const string MethodAttribute = "[TestMethod]";
        }

        /// <summary>
        /// Defines the XUnit type.
        /// </summary>
        public static class XUnit
        {
            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "XUnit";

            /// <summary>
            /// The class attribute.
            /// </summary>
            public const string ClassAttribute = "[Fact]";

            /// <summary>
            /// The method attribute.
            /// </summary>
            public const string MethodAttribute = "";
        }

        /// <summary>
        /// Defines the Moq type.
        /// </summary>
        public static class Moq
        {
            public const string Name = "Moq";

            /// <summary>
            /// The mocking variable declaration.
            /// </summary>
            public const string MockingVariableDeclaration = "Mock<%TYPE%>";

            /// <summary>
            /// The mock constructor code.
            /// </summary>
            public const string MockConstructorCode = ".Object";

            /// <summary>
            /// The mock init code.
            /// </summary>
            public const string MockInitCode = "new Mock";
        }

        /// <summary>
        /// Defines the RhinoMocks type.
        /// </summary>
        public static class RhinoMocks
        {
            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "RhinoMocks";

            /// <summary>
            /// The mock init code.
            /// </summary>
            public const string MockInitCode = "MockRepository.GenerateMock";
        }

        /// <summary>
        /// Defines the NSubstitute type.
        /// </summary>
        public static class NSubstitute
        {
            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "NSubstitute";

            /// <summary>
            /// The mock init code.
            /// </summary>
            public const string MockInitCode = "Substitute.For";
        }
    }
}
