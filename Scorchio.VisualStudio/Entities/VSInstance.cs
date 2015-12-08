// -----------------------------------------------------------------------
// <summary>
//   Defines the VSInstance type.
// </summary>
// -----------------------------------------------------------------------
namespace Scorchio.VisualStudio.Entities
{
    using EnvDTE80;

    /// <summary>
    ///   Defines the VSInstance type.
    /// </summary>
    public class VSInstance
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VSInstance" /> class.
        /// </summary>
        /// <param name="applicationObject">The application object.</param>
        public VSInstance(DTE2 applicationObject)
        {
            this.ApplicationObject = applicationObject;
        }

        /// <summary>
        /// Gets the ApplicationObject.
        /// </summary>
        public DTE2 ApplicationObject { get; private set; }
    }
}
