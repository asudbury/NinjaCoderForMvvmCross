// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using MvvmCross.Core.Platform;
    using MvvmCross.Core.Views;
    using MvvmCross.Platform.Core;
    using MvvmCross.Platform.Platform;
    using MvvmCross.Test.Core;
    using Mocks;
    using NUnit.Framework;

    /// <summary>
    /// Defines the BaseTest type.
    /// </summary>
    [TestFixture]
    public abstract class BaseTest : MvxIoCSupportingTest
    {
        /// <summary>
        /// The mock dispatcher.
        /// </summary>
        private MockDispatcher mockDispatcher;

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            this.ClearAll();

            this.mockDispatcher = new MockDispatcher();

            this.Ioc.RegisterSingleton<IMvxViewDispatcher>(this.mockDispatcher);
            this.Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(this.mockDispatcher);
            this.Ioc.RegisterSingleton<IMvxTrace>(new TestTrace());
            this.Ioc.RegisterSingleton<IMvxSettings>(new MvxSettings());

            this.Initialize();
            this.CreateTestableObject();
        }
        
        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            this.Terminate();
        }

        /// <summary>
        /// Creates the testable object.
        /// </summary>
        public virtual void CreateTestableObject()
        {
        }

        /// <summary>
        /// Initializes this instance.
        /// Any specific setup code for derived classes should override this method.
        /// </summary>
        protected virtual void Initialize()
        {
        }

        /// <summary>
        /// Terminates this instance.
        /// Any specific termination code for derived classes should override this method.
        /// </summary>
        protected virtual void Terminate()
        {
        }
    }
}
