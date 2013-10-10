// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the WebRequestSampleData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.WebRequestService.Entities
{
    /// <summary>
    ///  Defines the WebRequestSampleData type.
    /// </summary>
    public class WebRequestSampleData
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
