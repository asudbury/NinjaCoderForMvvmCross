// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NinjaCoderServiceException type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Exceptions
{
    /// <summary>
    ///  Defines the NinjaCoderServiceException type.
    /// </summary>
    public class NinjaCoderServiceException : NinjaCoderException
    {
        /// <summary>
        /// Gets or sets the name of the folder.
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string FileName { get; set; }
    }
}
