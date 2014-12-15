// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines the BaseTest type.
    /// </summary>
    [TestClass]
    public abstract class BaseTest 
    {
        /// <summary>
        /// Sets up.
        /// </summary>
        [TestInitialize]
        public virtual void SetUp()
        {
            this.Initialize();
            this.CreateTestableObject();
        }
                
        /// <summary>
        /// Tears down.
        /// </summary>
        [TestCleanup]
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
