// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    /// <summary>
    /// Defines the BaseTest type.
    /// </summary>
    public abstract class BaseTest 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTest"/> class.
        /// </summary>
        protected BaseTest()
        {
            this.SetUp();
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        public virtual void SetUp()
        {
            this.Initialize();
            this.CreateTestableObject();
        }

        /// <summary>
        /// Tears down.
        /// </summary>
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
