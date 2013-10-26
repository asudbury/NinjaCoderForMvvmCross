// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestFirstViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $safeprojectname$.ViewModels
{
    using Core.ViewModels;

    using NUnit.Framework;

    /// <summary>
    /// Defines the TestFirstViewModel type.
    /// </summary>
    [TestFixture]
    public class TestFirstViewModel : BaseTest
    {
        /// <summary>
        /// The first view model.
        /// </summary>
        private FirstViewModel firstViewModel;

        /// <summary>
        /// Creates an instance of the object to test.
        /// To allow Ninja automatically create the unit tests
        /// this method should not be changed.
        /// </summary>
        public override void CreateTestableObject()
        {
            this.firstViewModel = new FirstViewModel();
        }
        
        /// <summary>
        /// Tests my property.
        /// </summary>
        [Test]
        public void TestMyProperty()
        {
            //// arrange
            bool changed = false;

            this.firstViewModel.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == "MyProperty")
                    {
                        changed = true;
                    }
                };
            
            //// act
            this.firstViewModel.MyProperty = "Hello MvvmCross";

            //// assert
            Assert.AreEqual(changed, true);
        }

        /// <summary>
        /// Tests my command.
        /// </summary>
        [Test]
        public void TestMyCommand()
        {
            //// arrange

            //// act
            this.firstViewModel.MyCommand.Execute(null);

            //// assert
        }
    }
}
